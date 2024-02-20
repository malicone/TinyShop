using Microsoft.AspNetCore.Mvc;

namespace TinyShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
