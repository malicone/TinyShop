using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyShop.Data;
using TinyShop.Models;

namespace TinyShop.Controllers
{
    [Authorize]
    public class ProductGroupsController : Controller
    {
        private readonly ShopContext _context;

        public ProductGroupsController(ShopContext context)
        {
            _context = context;
        }

        // GET: ProductGroups
        public async Task<IActionResult> Index()
        {
            var groups = await _context.ProductGroups
                .Where( g => g.SoftDeletedAt.HasValue == false )
                .OrderBy( g => g.Name )
                .ToListAsync();
            foreach ( var group in groups )
            {
                group.ProductCount = _context.Products.Count( p => ( p.SoftDeletedAt.HasValue == false )
                    && ( p.ProductGroupId == group.Id ) );
            }
            return View( groups );
        }

        // GET: ProductGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups.FindAsync( id );
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // GET: ProductGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                productGroup.SetCreateStamp( User?.Identity?.Name );
                _context.Add(productGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productGroup);
        }

        // GET: ProductGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return NotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] ProductGroup productGroup)
        {
            if (id != productGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productGroup.SetUpdateStamp( User?.Identity?.Name );
                    _context.Update(productGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGroupExists(productGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productGroup);
        }

        // GET: ProductGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups.FindAsync( id );
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productGroup = await _context.ProductGroups.FindAsync(id);
            productGroup.SoftDelete( User?.Identity?.Name );            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductGroupExists(int id)
        {
            return _context.ProductGroups.Any(e => e.Id == id);
        }
    }
}