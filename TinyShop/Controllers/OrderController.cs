using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Infrastructure;
using TinyShop.Models;
using TinyShop.Models.ViewModels;

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
            var orders = await _context.Orders.Where( o => o.SoftDeletedAt.HasValue == false )
                .Include( o => o.TheCustomer ).OrderByDescending( o => o.OrderDateTime )
                .AsNoTracking().ToListAsync();
            return View( orders );
        }

        [AllowAnonymous]
        public async Task<ViewResult> Checkout()
        {
            var orderVM = new OrderViewModel();            
            orderVM.DeliveryTypes = await _context.DeliveryTypes
                .Where( d => d.SoftDeletedAt.HasValue == false ).OrderBy( d => d.SortingColumn )
                .AsNoTracking().ToListAsync();
            orderVM.PaymentTypes = await _context.PaymentTypes
                .Where( p => p.SoftDeletedAt.HasValue == false ).OrderBy( p => p.SortingColumn )
                .AsNoTracking().ToListAsync();            
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
                Order order = new Order();
                await PrepareOrderForInsert( orderVM, order );
                PrepareCustomerForInsertUpdate( orderVM, order );
                await PrepareDeliveryAddressForInsertUpdate( orderVM, order );
                _context.Orders.Add( order );
                await _context.SaveChangesAsync();

                _cart.Clear();
                orderVM.OrderId = order.Id;
                orderVM.OrderTotalSum = order.ComputeTotalValue();
                return View( "Completed", orderVM );
            }
            else
            {
                orderVM.DeliveryTypes = await _context.DeliveryTypes
                    .Where( d => d.SoftDeletedAt.HasValue == false ).OrderBy( d => d.SortingColumn )
                    .AsNoTracking().ToListAsync();
                orderVM.PaymentTypes = await _context.PaymentTypes
                    .Where( p => p.SoftDeletedAt.HasValue == false ).OrderBy( p => p.SortingColumn )
                    .AsNoTracking().ToListAsync();
                if (orderVM.DeliveryTypeId == DeliveryType.NovaPoshtaWarehouseId)
                {
                    orderVM.Regions = await _deliveryAddressProvider.GetRegionsAsync( orderVM.DeliveryTypeId );
                    orderVM.Cities = await _deliveryAddressProvider.GetCitiesByRegionAsync(
                        orderVM.DeliveryTypeId, orderVM.RegionId ?? 0 );
                    orderVM.Warehouses = await _deliveryAddressProvider.GetWarehousesByCityAsync(
                        orderVM.DeliveryTypeId, orderVM.CityId.Value );
                }
                return View(orderVM);
            }
        }

        private async Task PrepareOrderForInsert( OrderViewModel source, Order target )
        {
            target.SetCreateStamp( User?.Identity?.Name );
            target.TheOrderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(
                s => s.Id == OrderStatus.NewId );
            target.TheDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(
                d => d.Id == source.DeliveryTypeId );
            target.ThePaymentType = await _context.PaymentTypes.FirstOrDefaultAsync(
                p => p.Id == source.PaymentTypeId );
            target.Comments = source.Comments;
            target.Lines = new List<OrderLine>();
            foreach ( var cartLine in _cart.Lines )
            {
                OrderLine orderLine = new OrderLine
                {
                    TheProduct = cartLine.TheProduct,
                    PriceSnapshot = cartLine.PriceSnapshot,
                    Quantity = cartLine.Quantity
                };
                target.Lines.Add( orderLine );
            }
            // we need to get the product from the db to avoid the error:
            // "SqlException: Cannot insert explicit value for identity column in table 'Products' when
            // IDENTITY_INSERT is set to OFF."            
            foreach ( var line in target.Lines )
            {
                line.TheProduct = await _context.Products.FirstOrDefaultAsync( p => p.Id == line.TheProduct.Id );
                line.SetCreateStamp( User?.Identity?.Name );
            }
            target.OrderDateTime = DateTime.Now;            
        }

        private void PrepareCustomerForInsertUpdate( OrderViewModel source, Order target )
        {
            // LINQ doesn't work here (return null) so use loop
            //var foundCustomer = _context.Customers.FirstOrDefault( c => c.Equals( source.TheCustomer ) );            
            Customer foundCustomer = null;
            foreach ( var customer in _context.Customers )
            {
                if ( customer.Equals( source.TheCustomer ) )
                {
                    foundCustomer = customer;                    
                    break;
                }
            }            
            if ( foundCustomer == null )
            {
                // it's a new customer so we need to add it to the db
                target.TheCustomer = new Customer();
                target.TheCustomer.FirstName = source.TheCustomer.FirstName.Trim();
                target.TheCustomer.MiddleName = source.TheCustomer.MiddleName?.Trim();
                target.TheCustomer.LastName = source.TheCustomer.LastName.Trim();
                target.TheCustomer.Email = source.TheCustomer.Email?.Trim();
                target.TheCustomer.Phone = source.TheCustomer.Phone.Trim();
                target.TheCustomer.SetCreateStamp( User?.Identity?.Name );
                _context.Customers.Add( target.TheCustomer );
            }
            else
            {
                // customer already exists in the db so use it                
                target.TheCustomer = foundCustomer;
                bool emailIsNotEmpty = string.IsNullOrEmpty( source.TheCustomer.Email?.Trim() ) == false;
                if ( emailIsNotEmpty )
                {
                    foundCustomer.Email = source.TheCustomer.Email?.Trim();
                }
                foundCustomer.Phone = source.TheCustomer.Phone.Trim();// every time we update the phone to store actual data
                foundCustomer.SetUpdateStamp( User?.Identity?.Name );
                _context.Customers.Update( foundCustomer );                
            }
        }
        private async Task PrepareDeliveryAddressForInsertUpdate( OrderViewModel source, Order target )
        {
            if ( source.DeliveryTypeId != DeliveryType.NovaPoshtaWarehouseId )
            {
                return;// nothing to do for pickup
            }            
            var foundAddress = await _context.DeliveryAddresses.FirstOrDefaultAsync( 
                a => 
                a.TheRegion != null && a.TheCity != null && a.TheWarehouse != null &&
                a.TheRegion.Id.Equals( source.RegionId ) 
                && a.TheCity.Id.Equals( source.CityId ) 
                && a.TheWarehouse.Id.Equals( source.WarehouseId ) 
                && a.SoftDeletedAt.HasValue == false );
            if ( foundAddress == null )
            {
                target.TheDeliveryAddress = new DeliveryAddress();
                if ( source.RegionId != null )
                {
                    target.TheDeliveryAddress.TheRegion = await _context.Regions.FirstOrDefaultAsync(
                        r => r.Id == source.RegionId );
                    target.TheDeliveryAddress.RegionNameSnapshot = target.TheDeliveryAddress?.TheRegion?.Name;
                }
                if ( source.CityId != null )
                {
                    target.TheDeliveryAddress.TheCity = await _context.Cities.FirstOrDefaultAsync(
                        c => c.Id == source.CityId );
                    target.TheDeliveryAddress.CityNameSnapshot = target.TheDeliveryAddress?.TheCity?.Name;
                }
                if ( source.WarehouseId != null )
                {
                    target.TheDeliveryAddress.TheWarehouse = await _context.Warehouses.FirstOrDefaultAsync(
                        w => w.Id == source.WarehouseId );
                    target.TheDeliveryAddress.WarehouseNameSnapshot = target.TheDeliveryAddress?.TheWarehouse?.Name;
                }
                target.TheDeliveryAddress.SetCreateStamp( User?.Identity?.Name );
                _context.DeliveryAddresses.Add( target.TheDeliveryAddress );
            }
            else
            {
                target.TheDeliveryAddress = foundAddress;
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
                return Region.NovaPoshtaDefaultRegionId;
            }
            return 0;
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit( int? id )
        {
            if ( id == null )
            {
                return NotFound();
            }
            var order = await GetOrderFromDbWithIncludes( id.Value );
            if ( order == null )
            {
                return NotFound();
            }
            OrderViewModel orderVM = new OrderViewModel();
            orderVM.OrderId = order.Id;
            orderVM.TheCustomer = order.TheCustomer;
            orderVM.DeliveryTypeId = order.TheDeliveryType.Id;
            orderVM.PaymentTypeId = order.ThePaymentType.Id;
            orderVM.Comments = order.Comments;
            // It should be done so but it doesn't work (Include and Load don't work, see GetOrderFromDbWithIncludes)
            //orderVM.OrderLines = order.Lines;
            //orderVM.OrderTotalSum = order.ComputeTotalValue();
            //orderVM.OrderTotalQuantity = order.ComputeTotalQuantity();
            orderVM.OrderLines = await _context.OrderLines.Where( l => l.TheOrder.Id == orderVM.OrderId )
                .Include( l => l.TheProduct ).AsNoTracking().ToListAsync();               
            orderVM.OrderTotalSum = orderVM.OrderLines.Sum( l => l.PriceSnapshot * l.Quantity );
            orderVM.OrderTotalQuantity = orderVM.OrderLines.Sum( l => l.Quantity );
            await PrepareOrderVMDeliveryAndPayment( order, orderVM );
            return View( orderVM );
        }

        private async Task PrepareOrderVMDeliveryAndPayment( Order order, OrderViewModel orderVM )
        {
            orderVM.DeliveryTypes = await _context.DeliveryTypes
                .Where( d => d.SoftDeletedAt.HasValue == false ).OrderBy( d => d.SortingColumn )
                .AsNoTracking().ToListAsync();
            orderVM.PaymentTypes = await _context.PaymentTypes.Where( p => p.SoftDeletedAt.HasValue == false )
                .OrderBy( p => p.SortingColumn ).AsNoTracking().ToListAsync();
            if ( orderVM.DeliveryTypeId == DeliveryType.NovaPoshtaWarehouseId )
            {
                orderVM.RegionId = order.TheDeliveryAddress?.TheRegion?.Id;
                orderVM.CityId = order.TheDeliveryAddress?.TheCity?.Id;
                orderVM.WarehouseId = order.TheDeliveryAddress?.TheWarehouse?.Id;
                orderVM.Regions = await _deliveryAddressProvider.GetRegionsAsync( orderVM.DeliveryTypeId );
                orderVM.Cities = await _deliveryAddressProvider.GetCitiesByRegionAsync(
                    orderVM.DeliveryTypeId, orderVM.RegionId ?? 0 );
                orderVM.Warehouses = await _deliveryAddressProvider.GetWarehousesByCityAsync(
                    orderVM.DeliveryTypeId, orderVM.CityId.Value );
            }
        }

        public async Task<Order> GetOrderFromDbWithIncludes( int id )
        {
            var orders = await _context.Orders
                        .Include( o => o.TheCustomer )
                        .Include( o => o.TheDeliveryType )
                        .Include( o => o.ThePaymentType )
                        .Include( o => o.TheDeliveryAddress )
                        .Include( a => a.TheDeliveryAddress.TheRegion )
                        .Include( a => a.TheDeliveryAddress.TheCity )
                        .Include( a => a.TheDeliveryAddress.TheWarehouse )
                        .Include( o => o.Lines )
                        .FirstOrDefaultAsync( o => o.Id == id );
//            _context.Entry( orders ).Collection( o => o.Lines ).Load();
// Include and Load don't work so we just select order lines from the db (see Edit method)
            return orders;
        }

        // POST: Oder/Edit/
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( OrderViewModel orderVM )
        {
            if ( orderVM.OrderId == null )
            {
                return NotFound();
            }
            var order = await GetOrderFromDbWithIncludes( orderVM.OrderId.Value );
            if ( order == null )
            {
                return NotFound();
            }
            if ( ModelState.IsValid )
            {
                PrepareCustomerForInsertUpdate( orderVM, order );
                order.TheDeliveryType = await _context.DeliveryTypes.FirstOrDefaultAsync(
                    d => d.Id == orderVM.DeliveryTypeId );
                order.ThePaymentType = await _context.PaymentTypes.FirstOrDefaultAsync(
                    p => p.Id == orderVM.PaymentTypeId );
                if ( order.TheDeliveryType.Id == DeliveryType.NovaPoshtaWarehouseId )
                {
                    await PrepareDeliveryAddressForInsertUpdate( orderVM, order );
                }
                else// pickup
                {
                    if ( order.TheDeliveryAddress != null )
                    {
                        RemoveDeliveryAddress( order.TheDeliveryAddress.Id );
                    }
                    order.TheDeliveryAddress = null;
                }                
                order.Comments = orderVM.Comments;
                order.SetUpdateStamp( User?.Identity?.Name );
                _context.Orders.Update( order );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
            }
            else
            {
                await PrepareOrderVMDeliveryAndPayment( order, orderVM );
                return View( orderVM );
            }
        }

        private void RemoveDeliveryAddress( int id )
        {
            var deliveryAddress = _context.DeliveryAddresses.Find( id );
            if ( deliveryAddress != null )
            {
                var count = _context.Orders.Count( o => o.TheDeliveryAddress.Id == id );
                if ( count > 1 )
                {
                    return;
                }                
                deliveryAddress.SoftDelete( User?.Identity?.Name );
                _context.DeliveryAddresses.Update( deliveryAddress );                
            }
        }

        private readonly ShopContext _context;
        private readonly IDeliveryAddressProvider _deliveryAddressProvider;
        private readonly Cart _cart;
    }
}