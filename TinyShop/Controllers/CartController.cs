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
    [Route( "[controller]/[action]" )]
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

        /// <summary>
        /// Adds a product to the cart.
        /// </summary>
        /// <param name="productId">Id of the product to add.</param>
        /// <returns>CartSummaryComponent which can be used to refresh cart view.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("{productId}")]
        public IActionResult AddToCart(int productId)
        {
            Product? product = _context.Products.FirstOrDefault( p => p.Id == productId );
            if (product != null)
            {
                _cartVM.Cart.AddItem( product );
            }
            return ViewComponent( "CartSummary" );
        }
        [AllowAnonymous]
        [HttpGet]
        [Route( "{productId}" )]
        public IActionResult RemoveOneFromCart( int productId )
        {
            _cartVM.Cart.RemoveItem( productId );
            return ViewComponent( "CartSummary" );
        }

        [AllowAnonymous]
        [HttpGet]
        [Route( "{productId}" )]
        public IActionResult RemoveEntireLine( int productId )
        {
            _cartVM.Cart.RemoveLine( productId );
            return ViewComponent( "CartSummary" );
        }

        [AllowAnonymous]
        [HttpGet]        
        public IActionResult GetCartTableComponent()
        {
            return ViewComponent( "CartTable" );
        }

        private ILogger<HomeController> _logger;
        private ShopContext _context;
        private IWebHostEnvironment _appEnvironment;
        private CartViewModel _cartVM;
    }
}
