using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Models;

namespace TinyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _context;

        public HomeController( ILogger<HomeController> logger, ShopContext context )
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index( int? productGroupId )
        {
            var groups = from g in _context.ProductGroups orderby g.Name select g;
            var products = from p in _context.Products select p;
            ProductGroup foundGroup = null;

            if ( productGroupId != null )
            {
                products = products.Where( x => x.ProductGroupId == productGroupId );
                foundGroup = _context.ProductGroups.FirstOrDefault( g => g.Id == productGroupId );
            }

            var homeViewModel = new HomeViewModel
            {
                ProductGroups = await groups.ToListAsync(),
                Products = await products.ToListAsync(),
                CurrentGroup = foundGroup
            };

            foreach(var group in homeViewModel.ProductGroups)
            {
                _context.Entry( group ).Collection( g => g.Products ).Load();
            }            
            return View( homeViewModel );
        }

        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult Delivery()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}
