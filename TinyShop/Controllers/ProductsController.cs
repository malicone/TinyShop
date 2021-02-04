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
using TinyShop.Models;
using TinyShop.Utils;

namespace TinyShop.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductsController( ShopContext context, IWebHostEnvironment appEnvironment )
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Products.Include( p => p.ProductGroup );
            return View( await shopContext.ToListAsync() );
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
            ViewData[ "ProductGroupId" ] = new SelectList( _context.ProductGroups, "Id", "Name" );
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind( "Description,Name,Price,ProductGroupId,Id" )] Product product, IFormFileCollection photos )
        {
            if ( ModelState.IsValid )
            {
                if ( photos != null )
                {
                    await AddPhotos( photos, product );
                }
                _context.Add( product );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
            }
            ViewData[ "ProductGroupId" ] = new SelectList( _context.ProductGroups, "Id", "Name", product.ProductGroupId );
            return View( product );
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
            ViewData[ "ProductGroupId" ] = new SelectList( _context.ProductGroups, "Id", "Name", product.ProductGroupId );
            _context.Entry( product ).Collection( p => p.DescImages ).Load();
            return View( product );
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, [Bind( "Description,Name,Price,ProductGroupId,Id" )] Product product, IFormFileCollection photos )
        {
            if ( id != product.Id )
            {
                return NotFound();
            }

            if ( ModelState.IsValid )
            {
                try
                {
                    _context.Update( product );
                    if ( photos != null )
                    {
                        if ( photos.Count > 0 )
                        {
                            await AddPhotos( photos, product, true );
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch ( DbUpdateConcurrencyException )
                {
                    if ( !ProductExists( product.Id ) )
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
            ViewData[ "ProductGroupId" ] = new SelectList( _context.ProductGroups, "Id", "Name", product.ProductGroupId );
            return View( product );
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
            _context.Products.Remove( product );
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
        }

        private bool ProductExists( int id )
        {
            return _context.Products.Any( e => e.Id == id );
        }
    }
}
