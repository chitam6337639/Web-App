using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class AdminCateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminCateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminCate
        public async Task<IActionResult> Index()
        {
              return _context.Categoris != null ? 
                          View(await _context.Categoris.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Categoris'  is null.");
        }

        // GET: AdminCate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categoris == null)
            {
                return NotFound();
            }

            var category = await _context.Categoris
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [Authorize]
        // GET: AdminCate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminCate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: AdminCate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categoris == null)
            {
                return NotFound();
            }

            var category = await _context.Categoris.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: AdminCate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryDescription")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: AdminCate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categoris == null)
            {
                return NotFound();
            }

            var category = await _context.Categoris
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: AdminCate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categoris == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categoris'  is null.");
            }
            var category = await _context.Categoris.FindAsync(id);
            if (category != null)
            {
                _context.Categoris.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categoris?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
