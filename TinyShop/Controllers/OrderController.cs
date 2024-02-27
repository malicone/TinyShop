using Microsoft.AspNetCore.Mvc;
using RestSharp.Authenticators;
using RestSharp;
using System.Threading;
using TinyShop.Models;
using System.Threading.Tasks;
using TinyShop.RestUtils.NovaPoshta;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TinyShop.Controllers
{
    public class OrderController : Controller
    {
        public async Task<ViewResult> CheckoutAsync()
        {
            var order = new Order();
            NovaPoshtaClient npClient = new NovaPoshtaClient();
            order.Regions = await npClient.GetRegionsAsync();
            ViewData["RegionId"] = new SelectList( order.Regions, "Id", "Name" );
            return View( order );
        }
    }
}
