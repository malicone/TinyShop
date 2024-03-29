using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Infrastructure;
using TinyShop.Models;
using TinyShop.Models.ViewModels;
using TinyShop.RestUtils.NovaPoshta;

namespace TinyShop.Controllers
{
    [Authorize]
    [Route( "[controller]/[action]" )]
    public class OrderController : Controller
    {
        public OrderController( ShopContext context, IDeliveryAddressProvider deliveryAddressProvider, Cart cart )
        {
            _context = context;            
            _cart = cart;
            _deliveryAddressProvider = deliveryAddressProvider;
        }
        
        public async Task<ViewResult> Index()
        {
            return View( await _context.Orders.Where( o => o.SoftDeletedAt.HasValue == false )
                .OrderByDescending( o => o.OrderDateTime ).ToListAsync() );
        }

        [AllowAnonymous]
        public async Task<ViewResult> Checkout()
        {
            var orderVM = new OrderViewModel();            
            orderVM.DeliveryTypes = await _context.DeliveryTypes
                .Where( d => d.SoftDeletedAt.HasValue == false ).OrderBy( d => d.SortingColumn ).ToListAsync();
            orderVM.PaymentTypes = await _context.PaymentTypes
                .Where( p => p.SoftDeletedAt.HasValue == false ).OrderBy( p => p.SortingColumn ).ToListAsync();            
            return View( orderVM );
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Checkout(OrderViewModel orderVM)
        {
            if (_cart.Lines.Count == 0)
            {
                ModelState.AddModelError( string.Empty, "Ваш кошик пустий" );
            }
            if (ModelState.IsValid)
            {
                await PrepareOrderForInsert( orderVM.TheOrder );
                PrepareCustomerForInsertUpdate( orderVM.TheOrder );
                PrepareDeliveryAddressForInsertUpdate( orderVM.TheOrder );
                _context.Add( orderVM.TheOrder );
                await _context.SaveChangesAsync();

                _cart.Clear();
                return View( "Completed", orderVM );
            }
            else
            {
                orderVM.DeliveryTypes = await _context.DeliveryTypes.ToListAsync();
                orderVM.PaymentTypes = await _context.PaymentTypes.ToListAsync();                
                return View(orderVM);
            }
        }

        private async Task PrepareOrderForInsert( Order order )
        {
            order.SetCreateStamp( User?.Identity?.Name );
            order.TheOrderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(
                s => s.Id == OrderStatus.NewId );
            order.TheDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(
                d => d.Id == order.TheDeliveryType.Id );
            order.ThePaymentType = await _context.PaymentTypes.FirstOrDefaultAsync(
                p => p.Id == order.ThePaymentType.Id );
            order.Lines = _cart.Lines;
            // we need to get the product from the db to avoid the error:
            // "SqlException: Cannot insert explicit value for identity column in table 'Products' when
            // IDENTITY_INSERT is set to OFF."
            foreach ( var line in order.Lines )
            {
                line.TheProduct = await _context.Products.FirstOrDefaultAsync( p => p.Id == line.TheProduct.Id );
                line.SetCreateStamp( User?.Identity?.Name );
            }
            order.OrderDateTime = System.DateTime.Now;            
        }

        private void PrepareCustomerForInsertUpdate( Order order )
        {
            // LINQ doesn't work here so use loop
            //var foundCustomer = await _context.Customers.FirstOrDefaultAsync(
            //    customer => customer == order.TheCustomer );                
            Customer foundCustomer = null;
            foreach ( var customer in _context.Customers )
            {
                if ( customer.Equals( order.TheCustomer ) )
                {
                    foundCustomer = customer;
                    break;
                }
            }
            if ( foundCustomer == null )
            {
                // it's a new customer so we need to add it to the db
                order.TheCustomer.FirstName = order.TheCustomer.FirstName.Trim();
                order.TheCustomer.MiddleName = order.TheCustomer.MiddleName?.Trim();
                order.TheCustomer.LastName = order.TheCustomer.LastName.Trim();
                order.TheCustomer.Email = order.TheCustomer.Email.Trim();
                order.TheCustomer.Phone = order.TheCustomer.Phone.Trim();
                order.TheCustomer.SetCreateStamp( User?.Identity?.Name );
                _context.Add( order.TheCustomer );
            }
            else
            {
                // customer already exists in the db so use it                
                bool emailIsNotEmpty = string.IsNullOrEmpty( order.TheCustomer.Email.Trim() ) == false;
                if ( emailIsNotEmpty )
                {
                    foundCustomer.Email = order.TheCustomer.Email.Trim();
                }
                foundCustomer.Phone = order.TheCustomer.Phone.Trim();
                foundCustomer.SetUpdateStamp( User?.Identity?.Name );
                _context.Update( foundCustomer );
            }
        }
        private async Task PrepareDeliveryAddressForInsertUpdate( Order order )
        {
            if ( order.TheDeliveryAddress == null )
            {
                return;
            }            
            var foundAddress = await _context.DeliveryAddresses.FirstOrDefaultAsync( 
                a => a.Equals( order.TheDeliveryAddress ) );
            if ( foundAddress == null )
            {
                if ( order.TheDeliveryAddress.TheRegion != null )
                {
                    order.TheDeliveryAddress.TheRegion = await _context.Regions.FirstOrDefaultAsync(
                        r => r.Id == order.TheDeliveryAddress.TheRegion.Id );
                    order.TheDeliveryAddress.RegionNameSnapshot = order.TheDeliveryAddress.TheRegion.Name;
                }
                if ( order.TheDeliveryAddress.TheCity != null )
                {
                    order.TheDeliveryAddress.TheCity = await _context.Cities.FirstOrDefaultAsync(
                        c => c.Id == order.TheDeliveryAddress.TheCity.Id );
                    order.TheDeliveryAddress.CityNameSnapshot = order.TheDeliveryAddress.TheCity.Name;
                }
                if ( order.TheDeliveryAddress.TheWarehouse != null )
                {
                    order.TheDeliveryAddress.TheWarehouse = await _context.Warehouses.FirstOrDefaultAsync(
                        w => w.Id == order.TheDeliveryAddress.TheWarehouse.Id );
                    order.TheDeliveryAddress.WarehouseNameSnapshot = order.TheDeliveryAddress.TheWarehouse.Name;
                }
                order.TheDeliveryAddress.SetCreateStamp( User?.Identity?.Name );
                _context.Add( order.TheDeliveryAddress );
            }
            else
            {
                order.TheDeliveryAddress = foundAddress;
            }
        }

        [AllowAnonymous]
        [Route( "{deliveryTypeId}" )]
        public async Task<JsonResult> GetRegions( int deliveryTypeId )
        {
            return Json( await _deliveryAddressProvider.GetRegionsAsync( deliveryTypeId ) );
        }

        [AllowAnonymous]
        [Route( "{deliveryTypeId}/{regionId}" )]
        public async Task<JsonResult> GetCitiesByRegion( int deliveryTypeId, int regionId )
        {
            return Json( await _deliveryAddressProvider.GetCitiesByRegionAsync( deliveryTypeId, regionId ) );
        }

        [AllowAnonymous]
        [Route( "{deliveryTypeId}/{cityId}" )]
        public async Task<JsonResult> GetWarehousesByCity( int deliveryTypeId, int cityId )
        {            
            return Json( await _deliveryAddressProvider.GetWarehousesByCityAsync( deliveryTypeId, cityId ) );
        }

        [AllowAnonymous]
        [Route( "{deliveryTypeId}" )]
        public int GetDefaultRegionId( int deliveryTypeId )
        {
            if ( deliveryTypeId == DeliveryType.NovaPoshtaWarehouseId )
            {
                return Order.NovaPoshtaDefaultRegionId;
            }
            return 0;
        }
        
        private readonly ShopContext _context;
        private readonly IDeliveryAddressProvider _deliveryAddressProvider;
        private readonly Cart _cart;
    }
}