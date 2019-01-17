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
using ProjectC_v2.Services;

namespace ProjectC_v2.Controllers
{
    [Authorize(Roles = "Admin")]
    public sealed partial class InventoriesController : Controller
    {
        private readonly WebshopDbContext _db;

        public InventoriesController(WebshopDbContext db)
        {
            _db = db;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            var webshopDbContext = _db.Inventory.Include(i => i.GamePlatform).Include(i => i.Product);
            return View(await webshopDbContext.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _db.Inventory
                .Include(i => i.GamePlatform)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["PlatformId"] = new SelectList(_db.GamePlatform, "PlatformId", "PlatformName");
            ViewData["ProductId"] = new SelectList(_db.Product, "ProductId", "Name");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///  TODO : Validate existing records.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryId,Stock,Price,ProductId,PlatformId")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _db.Add(inventory);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlatformId"] = new SelectList(_db.GamePlatform, "PlatformId", "PlatformName", inventory.PlatformId);
            ViewData["ProductId"] = new SelectList(_db.Product, "ProductId", "Name", inventory.ProductId);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _db.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["PlatformId"] = new SelectList(_db.GamePlatform, "PlatformId", "PlatformName", inventory.PlatformId);
            ViewData["ProductId"] = new SelectList(_db.Product, "ProductId", "Name", inventory.ProductId);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Validate for existing records...
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryId,Stock,Price,ProductId,PlatformId")] Inventory inventory)
        {
            if (id != inventory.InventoryId)
            {
                return NotFound();
            }
            ModelState.Remove("PlatformId");
            ModelState.Remove("ProductId");
            if (ModelState.IsValid && await InventoriesService.ValidEditAsync(inventory , _db))
            {
                try
                {
                    _db.Update(inventory);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.InventoryId))
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
            ViewData["PlatformId"] = new SelectList(_db.GamePlatform, "PlatformId", "PlatformName", inventory.PlatformId);
            ViewData["ProductId"] = new SelectList(_db.Product, "ProductId", "Name", inventory.ProductId);
            ModelState.AddModelError("", "Something went wrong");
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _db.Inventory
                .Include(i => i.GamePlatform)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _db.Inventory.FindAsync(id);
            //_db.Inventory.Remove(inventory);
            inventory.Stock = 0;
            _db.Update(inventory);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Hide this method within a Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool InventoryExists(int id)
        {
            return _db.Inventory.Any(e => e.InventoryId == id);
        }
    }
    [Authorize(Roles = "Admin")]
    public sealed partial class InventoriesController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> ValidatePPCombination(int ProductId , int PlatformId)
        {
            try
            {
                return Json(! await InventoriesService.RecordExists(ProductId , PlatformId , _db));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
