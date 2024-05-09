using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TinyShop.Data;
using TinyShop.Models.ViewModels;
using TinyShop.Models;

namespace TinyShop.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public NavigationMenuViewComponent( ShopContext context  )
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ProductGroupViewModel viewModel = new ProductGroupViewModel();
            string selectedGroupIdAsString = HttpContext.Request.Query[ "productGroupId" ];
            int selectedGroupId = 0;
            if ( int.TryParse( selectedGroupIdAsString, out selectedGroupId ) )
            {
                viewModel.SelectedGroupId = selectedGroupId;
            }            

            viewModel.ProductGroups = _context.ProductGroups.Where( g => g.SoftDeletedAt.HasValue == false )
                .OrderBy( g => g.Name ).AsNoTracking().ToList();
            viewModel.ProductGroups.Insert( 0, new ProductGroup { Name = "Усі товари", Id = 0 } );
            foreach ( var productGroup in viewModel.ProductGroups )
            {
                productGroup.ProductCount = _context.Products.Where( 
                    p => ( p.ProductGroupId == productGroup.Id ) && ( p.SoftDeletedAt.HasValue == false ) ).Count();
            }
            viewModel.ProductGroups[ 0 ].ProductCount = _context.Products
                .Where( p => p.SoftDeletedAt.HasValue == false ).Count();
            
            return View( viewModel );
        }

        private readonly ShopContext _context;
    }
}
