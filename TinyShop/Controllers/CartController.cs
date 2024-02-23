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
        public CartController(ILogger<HomeController> logger, ShopContext context, 
            IWebHostEnvironment appEnvironment, CartModel cartModel)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _logger = logger;
            _cartModel = cartModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            _cartModel.ReturnUrl = returnUrl;
            return View( _cartModel );
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(int productId, string returnUrl)
        {            
            Product? product = _context.Products.FirstOrDefault( p => p.Id == productId );
            if (product != null)
            {                
                _cartModel.Cart.AddItem( product, 1 );
            }                    
            _cartModel.ReturnUrl = returnUrl;
            return View( _cartModel );
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Remove(int productId, string returnUrl)
        {
            _cartModel.Cart.RemoveLine( _cartModel.Cart.Lines.First( cl => cl.Product.Id == productId ).Product );            
            return Redirect( returnUrl );
        }

        private ILogger<HomeController> _logger;
        private ShopContext _context;
        private IWebHostEnvironment _appEnvironment;
        private CartModel _cartModel;
    }
}
