using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Models;
using TinyShop.Models.ViewModels;
using TinyShop.RestUtils.NovaPoshta;

namespace TinyShop.Controllers
{
    [Authorize]
    [Route( "[controller]/[action]" )]
    public class OrderController : Controller
    {
        public OrderController( ShopContext context, NovaPoshtaClient npClient, Cart cart )
        {
            _context = context;
            _npClient = npClient;
            _cart = cart;
        }

        [AllowAnonymous]
        public async Task<ViewResult> Checkout()
        {
            var orderVM = new OrderViewModel();            
            orderVM.DeliveryTypes = await _context.DeliveryTypes.ToListAsync();
            orderVM.PaymentTypes = await _context.PaymentTypes.ToListAsync();            
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
                await PrepareForInsert( orderVM.TheOrder );
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

        private async Task PrepareForInsert( Order order )
        {
            order.SetCreateStamp( User.Identity.Name );
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
                line.SetCreateStamp( User.Identity.Name );
            }
            order.OrderDateTime = System.DateTime.Now;
            // LINQ doesn't work here so use loop
            //var foundCustomer = await _context.Customers.FirstOrDefaultAsync( customer => customer == orderVM.TheOrder.TheCustomer );                
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
                order.TheCustomer.SetCreateStamp( User.Identity.Name );
                _context.Add( order.TheCustomer );
            }
            else
            {
                // customer already exists in the db so use it
                order.TheCustomer = foundCustomer;
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetRegions()
        {
            return Json( await _npClient.GetRegionsAsync() );
        }

        [AllowAnonymous]
        [Route( "{regionIdExternal}" )]
        public async Task<JsonResult> GetCitiesByRegion( string regionIdExternal )
        {            
            return Json(await _npClient.GetCitiesByRegionAsync(regionIdExternal));
        }

        [AllowAnonymous]
        [Route( "{cityIdExternal}" )]
        public async Task<JsonResult> GetWarehousesByCity(string cityIdExternal)
        {            
            return Json( await _npClient.GetWarehousesByCityAsync( cityIdExternal ) );
        }
        
        private readonly ShopContext _context;
        private readonly NovaPoshtaClient _npClient;
        private readonly Cart _cart;
    }
}