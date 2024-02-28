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

namespace TinyShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        [AllowAnonymous]
        public async Task<ViewResult> CheckoutAsync()
        {
            var orderVM = new OrderViewModel();
            NovaPoshtaClient npClient = new NovaPoshtaClient();
            orderVM.Regions = await npClient.GetRegionsAsync();            
            return View( orderVM );
        }

        [AllowAnonymous]        
        public async Task<JsonResult> GetCitiesByRegion( string regionId )
        {
            //return Json( "[{\"id\": \"1\", \"Name\": \"Lutsk\"}, {\"id\": \"2\", \"Name\": \"Kyiv\"}]" );
            //return Json( new { Id = 123, Name = "Hero" } );
            NovaPoshtaClient npClient = new NovaPoshtaClient();
            return Json(await npClient.GetCitiesByRegionAsync(regionId));
        }
    }
}
