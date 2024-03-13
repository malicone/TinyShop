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
            orderVM.Regions = await _npClient.GetRegionsAsync();            
            return View( orderVM );
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Checkout(OrderViewModel orderVM)
        {
            if (_cart.Lines.Count == 0)
            {
                ModelState.AddModelError( string.Empty, "Sorry, your cart is empty!" );
            }
            if (ModelState.IsValid)
            {
                orderVM.TheOrder.Lines = _cart.Lines.ToArray();                
                _cart.Clear();
                return View( "Completed", orderVM );
            }
            else
            {
                orderVM.Regions = _npClient.GetRegionsAsync().Result;
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