using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Infrastructure;
using TinyShop.Models;
using TinyShop.Models.ViewModels;

namespace TinyShop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController( ILogger<HomeController> logger, ShopContext context, IWebHostEnvironment appEnvironment )
        {
            _logger = logger;
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index( int? productGroupId, int productPage = 1 )
        {
            _logger.LogInformation( "HomeController.Index started" );
        
            int itemsPerPage = GlobalOptions.DefaultItemsPerPage;
            IQueryable<Product> products;
            int totalItems = 0;
            if ( ( productGroupId == null ) || ( productGroupId.Value == 0 ) )
            {
                // Select all products
                products = _context.Products.Where( p => p.SoftDeletedAt.HasValue == false )
                    .Include( p => p.Prices )
                    .AsNoTracking().OrderByDescending( p => p.Id )
                    .Skip( ( productPage - 1 ) * itemsPerPage )
                    .Take( itemsPerPage );                
                totalItems = _context.Products.Count(p => p.SoftDeletedAt.HasValue == false);
            }
            else
            {
                // Select products of specified group
                products = _context.Products.Where( 
                    p => ( p.SoftDeletedAt.HasValue == false ) && ( p.ProductGroupId == productGroupId ) )
                    .Include( p => p.Prices )
                    .AsNoTracking().OrderByDescending( p => p.Id )
                    .Skip( ( productPage - 1 ) * itemsPerPage )
                    .Take( itemsPerPage );
                totalItems = _context.Products.Count(p => 
                    ( p.SoftDeletedAt.HasValue == false ) && ( p.ProductGroupId == productGroupId ));
            }
            var homeViewModel = new HomeViewModel
            {
                Products = await products.ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = itemsPerPage,
                    TotalItems = totalItems
                },
                CurrentProductGroupId = productGroupId ?? 0
            };
            return View( homeViewModel );
        }

        [AllowAnonymous]
        public IActionResult Payment()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Delivery()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if ( exceptionFeature != null )
            {
                _logger.LogError( exceptionFeature.Error, $"Request path: {exceptionFeature.Path}" );
            }
            ErrorViewModel evm = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            evm.Message = "Error message: " + exceptionFeature.Error.Message;
            return View( evm );
        }

        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
    }
}
