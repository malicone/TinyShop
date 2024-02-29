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
    public class OrderController : Controller
    {
        public OrderController( ShopContext context, NovaPoshtaClient npClient )
        {
            _context = context;
            _npClient = npClient;
        }

        [AllowAnonymous]
        public async Task<ViewResult> Checkout()
        {
            var orderVM = new OrderViewModel();            
            orderVM.Regions = await _npClient.GetRegionsAsync();            
            return View( orderVM );
        }

        [AllowAnonymous]        
        public async Task<JsonResult> GetCitiesByRegion( string regionId )
        {            
            return Json(await _npClient.GetCitiesByRegionAsync(regionId));
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetWarehousesByCity(string cityId)
        {            
            return Json( await _npClient.GetWarehousesByCityAsync( cityId ) );
        }
        
        private readonly ShopContext _context;
        private readonly NovaPoshtaClient _npClient;
    }
}
