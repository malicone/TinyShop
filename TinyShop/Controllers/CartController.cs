using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using TinyShop.Data;
using TinyShop.Infrastructure;
using TinyShop.Models;

namespace TinyShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public CartController(ILogger<HomeController> logger, ShopContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(int productId, string returnUrl)
        {
            CartModel cartModel = new CartModel();
            Product? product = _context.Products.FirstOrDefault( p => p.Id == productId );
            if (product != null)
            {                
                cartModel.Cart = HttpContext.Session.GetJson<Cart>( GlobalConstants.CartSessionId ) ?? new Cart();                
                cartModel.Cart.AddItem( product, 1 );
                HttpContext.Session.SetJson( GlobalConstants.CartSessionId, cartModel.Cart );
            }                    
            cartModel.ReturnUrl = returnUrl;
            return View( cartModel );
        }

        private ILogger<HomeController> _logger;
        private ShopContext _context;
        private IWebHostEnvironment _appEnvironment;
    }
}
