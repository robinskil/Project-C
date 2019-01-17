using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectC_v2.Database;
using ProjectC_v2.Models;

namespace ProjectC_v2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductFeaturesController : Controller
    {
        private readonly WebshopDbContext _context;

        public ProductFeaturesController(WebshopDbContext context)
        {
            _context = context;
        }

        // GET: ProductFeatures
        public async Task<IActionResult> Index()
        {
            var webshopDbContext = _context.ProductFeature.Include(p => p.Product);
            return View(await webshopDbContext.ToListAsync());
        }

        // GET: ProductFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFeature = await _context.ProductFeature
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.FeatureId == id);
            if (productFeature == null)
            {
                return NotFound();
            }

            return View(productFeature);
        }

        // GET: ProductFeatures/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            return View();
        }

        // POST: ProductFeatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeatureId,Feature,ProductId")] ProductFeature productFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productFeature.ProductId);
            return View(productFeature);
        }

        // GET: ProductFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFeature = await _context.ProductFeature.FindAsync(id);
            if (productFeature == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productFeature.ProductId);
            return View(productFeature);
        }

        // POST: ProductFeatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeatureId,Feature,ProductId")] ProductFeature productFeature)
        {
            if (id != productFeature.FeatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductFeatureExists(productFeature.FeatureId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productFeature.ProductId);
            return View(productFeature);
        }

        // GET: ProductFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFeature = await _context.ProductFeature
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.FeatureId == id);
            if (productFeature == null)
            {
                return NotFound();
            }

            return View(productFeature);
        }

        // POST: ProductFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productFeature = await _context.ProductFeature.FindAsync(id);
            _context.ProductFeature.Remove(productFeature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductFeatureExists(int id)
        {
            return _context.ProductFeature.Any(e => e.FeatureId == id);
        }
    }
}
