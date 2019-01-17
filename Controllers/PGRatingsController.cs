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
    public class PGRatingsController : Controller
    {
        private readonly WebshopDbContext _context;

        public PGRatingsController(WebshopDbContext context)
        {
            _context = context;
        }

        // GET: PGRatings
        public async Task<IActionResult> Index()
        {
            return View(await _context.PGRating.ToListAsync());
        }

        // GET: PGRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pGRating = await _context.PGRating
                .FirstOrDefaultAsync(m => m.PGRatingId == id);
            if (pGRating == null)
            {
                return NotFound();
            }

            return View(pGRating);
        }

        // GET: PGRatings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PGRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PGRatingId,Rating")] PGRating pGRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pGRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pGRating);
        }

        // GET: PGRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pGRating = await _context.PGRating.FindAsync(id);
            if (pGRating == null)
            {
                return NotFound();
            }
            return View(pGRating);
        }

        // POST: PGRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PGRatingId,Rating")] PGRating pGRating)
        {
            if (id != pGRating.PGRatingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pGRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PGRatingExists(pGRating.PGRatingId))
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
            return View(pGRating);
        }

        //// GET: PGRatings/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pGRating = await _context.PGRating
        //        .FirstOrDefaultAsync(m => m.PGRatingId == id);
        //    if (pGRating == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(pGRating);
        //}

        //// POST: PGRatings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var pGRating = await _context.PGRating.FindAsync(id);
        //    _context.PGRating.Remove(pGRating);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PGRatingExists(int id)
        {
            return _context.PGRating.Any(e => e.PGRatingId == id);
        }
    }
}
