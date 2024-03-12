using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyShop.Data;
using TinyShop.Infrastructure;
using TinyShop.Models;
using TinyShop.Models.ViewModels;

namespace TinyShop.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        public ProductsController( ShopContext context, IWebHostEnvironment appEnvironment )
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = _context.Products.Include( p => p.ProductGroup )
                .Where( p => p.SoftDeletedAt.HasValue == false );
            return View( await products.ToListAsync() );
        }

        // GET: Products/Details/5        
        public async Task<IActionResult> Details( int? id )
        {
            if ( id == null )
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include( p => p.ProductGroup )
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( product == null )
            {
                return NotFound();
            }

            _context.Entry( product ).Collection( p => p.DescImages ).Load();
            return View( product );
        }

        [AllowAnonymous]
        public async Task<IActionResult> DetailsReadOnly( int? id )
        {
            if ( id == null )
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include( p => p.ProductGroup )
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( product == null )
            {
                return NotFound();
            }

            _context.Entry( product ).Collection( p => p.DescImages ).Load();
            return View( product );
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ProductViewModel productVM = new ProductViewModel();
            productVM.ProductGroups = _context.ProductGroups.ToList();
            return View( productVM );
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind( "TheProduct" )] ProductViewModel productVM, 
            IFormFileCollection photos )
        {
            if ( ModelState.IsValid )
            {
                if ( photos != null )
                {
                    await AddPhotos( photos, productVM.TheProduct );
                }
                productVM.TheProduct.SetCreateStamp( User.Identity.Name );
                _context.Add( productVM.TheProduct );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
            }
            productVM.ProductGroups = _context.ProductGroups.ToList();
            return View( productVM );
        }

        private async Task AddPhotos( IFormFileCollection photos, Product product, bool removeOldPhotos = false )
        {
            if ( removeOldPhotos )
            {
                _context.Entry( product ).Collection( p => p.DescImages ).Load();
                _context.RemoveRange( product.DescImages );
            }
            foreach ( var currentFile in photos )
            {
                FileTag file = new FileTag();
                file.Name = currentFile.FileName;
                file.Ext = IOUtils.GetExtensionWithoutDot( currentFile.FileName );
                using ( MemoryStream stream = new MemoryStream() )
                {
                    await currentFile.CopyToAsync( stream );
                    file.Body = stream.ToArray();
                    file.Length = stream.Length;
                }
                product.DescImages.Add( file );
            }
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit( int? id )
        {
            if ( id == null )
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync( id );
            if ( product == null )
            {
                return NotFound();
            }
            ProductViewModel productVM = new ProductViewModel();
            productVM.ProductGroups = _context.ProductGroups.ToList();
            _context.Entry( product ).Collection( p => p.DescImages ).Load();
            productVM.TheProduct = product;
            return View( productVM );
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, [Bind( "TheProduct" )] ProductViewModel productVM, 
            IFormFileCollection photos )
        {
            if ( id != productVM.TheProduct.Id )
            {
                return NotFound();
            }

            if ( ModelState.IsValid )
            {
                try
                {
                    productVM.TheProduct.SetUpdateStamp( User.Identity.Name );
                    _context.Update( productVM.TheProduct );
                    if ( photos != null )
                    {
                        if ( photos.Count > 0 )
                        {
                            await AddPhotos( photos, productVM.TheProduct, true );
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch ( DbUpdateConcurrencyException )
                {
                    if ( !ProductExists( productVM.TheProduct.Id ) )
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction( nameof( Index ) );
            }
            productVM.ProductGroups = _context.ProductGroups.ToList();
            return View( productVM );
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete( int? id )
        {
            if ( id == null )
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include( p => p.ProductGroup )
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( product == null )
            {
                return NotFound();
            }

            return View( product );
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed( int id )
        {
            var product = await _context.Products.FindAsync( id );
            product.SoftDelete( User.Identity.Name );            
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
        }

        private bool ProductExists( int id )
        {
            return _context.Products.Any( e => e.Id == id );
        }

        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
    }
}
