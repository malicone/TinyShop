﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Index( int? productGroupId )
        {
            _logger.LogInformation( "HomeController.Index started" );
        
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
