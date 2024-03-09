using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using TinyShop.Data;
using TinyShop.Infrastructure;
using TinyShop.Models;
using TinyShop.Models.ViewModels;

namespace TinyShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public CartController(ILogger<HomeController> logger, ShopContext context, 
            IWebHostEnvironment appEnvironment, CartViewModel cartViewModel)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _logger = logger;
            _cartVM = cartViewModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            _cartVM.ReturnUrl = returnUrl;
            return View( _cartVM );
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(int productId, string returnUrl)
        {            
            Product? product = _context.Products.FirstOrDefault( p => p.Id == productId );
            if (product != null)
            {                
                _cartVM.Cart.AddItem( product, 1 );
            }                    
            _cartVM.ReturnUrl = returnUrl;
            return View( _cartVM );
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Remove(int productId, string returnUrl)
        {
            _cartVM.Cart.RemoveLine( _cartVM.Cart.Lines.First( cl => cl.TheProduct.Id == productId ).TheProduct );            
            return Redirect( returnUrl );
        }

        private ILogger<HomeController> _logger;
        private ShopContext _context;
        private IWebHostEnvironment _appEnvironment;
        private CartViewModel _cartVM;
    }
}
