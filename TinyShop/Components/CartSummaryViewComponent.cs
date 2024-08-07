﻿using Microsoft.AspNetCore.Mvc;
using TinyShop.Models;

namespace TinyShop.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {        
        public CartSummaryViewComponent(Cart cart)
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
