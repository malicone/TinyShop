using Microsoft.AspNetCore.Mvc;
using RestSharp.Authenticators;
using RestSharp;
using System.Threading;
using TinyShop.Models;
using System.Threading.Tasks;
using TinyShop.RestUtils.NovaPoshta;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using TinyShop.Data;

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
                orderVM.TheOrder.SetCreateStamp( User.Identity.Name );
                orderVM.TheOrder.TheOrderStatus = await _context.OrderStatuses.FirstOrDefaultAsync( 
                    s => s.Id == OrderStatus.NewId );
                orderVM.TheOrder.Lines = _cart.Lines;
                orderVM.TheOrder.OrderDateTime = System.DateTime.Now;
                var foundCustomer = await _context.Customers.FirstOrDefaultAsync( c => c == orderVM.TheOrder.TheCustomer );
                if (foundCustomer == null)
                {
                    // it's a new customer so we need to add it to the db
                    _context.Add( orderVM.TheOrder.TheCustomer );                    
                }
                else
                {
                    // customer already exists in the db so use it
                    orderVM.TheOrder.TheCustomer = foundCustomer;
                }                
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

        [AllowAnonymous]
        public async Task<JsonResult> GetRegions()
        {
            return Json( await _npClient.GetRegionsAsync() );
        }

        [AllowAnonymous]
        [Route( "{regionId}" )]
        public async Task<JsonResult> GetCitiesByRegion( string regionId )
        {            
            return Json(await _npClient.GetCitiesByRegionAsync(regionId));
        }

        [AllowAnonymous]
        [Route( "{cityId}" )]
        public async Task<JsonResult> GetWarehousesByCity(string cityId)
        {            
            return Json( await _npClient.GetWarehousesByCityAsync( cityId ) );
        }
        
        private readonly ShopContext _context;
        private readonly NovaPoshtaClient _npClient;
        private readonly Cart _cart;
    }
}