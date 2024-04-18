using Microsoft.AspNetCore.Mvc;
using TinyShop.Models;

namespace TinyShop.Components
{
    public class CartTableViewComponent : ViewComponent
    {
        public CartTableViewComponent( Cart cart )
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {
            return View( _cart );
        }

        private Cart _cart;
    }
}
