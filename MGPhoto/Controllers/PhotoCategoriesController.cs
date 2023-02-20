using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MGPhoto.Models;
using Microsoft.AspNetCore.Authorization;

namespace MGPhoto.Controllers
{
    public class PhotoCategoriesController : Controller
    {
        private readonly MGPhotoDbContext _context;

        public PhotoCategoriesController(MGPhotoDbContext context)
        {
            _context = context;
        }

        // GET: PhotoCategories
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
              return _context.PhotoCategories != null ? 
                          View(await _context.PhotoCategories.ToListAsync()) :
                          Problem("Entity set 'MGPhotoDbContext.PhotoCategories'  is null.");
        }

        // GET: PhotoCategories/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhotoCategories == null)
            {
                return NotFound();
            }

            var photoCategory = await _context.PhotoCategories
                .FirstOrDefaultAsync(m => m.IdCategory == id);
            if (photoCategory == null)
            {
                return NotFound();
            }

            return View(photoCategory);
        }

        // GET: PhotoCategories/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhotoCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("IdCategory,Name,Description")] PhotoCategory photoCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photoCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photoCategory);
        }

        // GET: PhotoCategories/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhotoCategories == null)
            {
                return NotFound();
            }

            var photoCategory = await _context.PhotoCategories.FindAsync(id);
            if (photoCategory == null)
            {
                return NotFound();
            }
            return View(photoCategory);
        }

        // POST: PhotoCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategory,Name,Description")] PhotoCategory photoCategory)
        {
            if (id != photoCategory.IdCategory)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photoCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoCategoryExists(photoCategory.IdCategory))
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
            return View(photoCategory);
        }

        // GET: PhotoCategories/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhotoCategories == null)
            {
                return NotFound();
            }

            var photoCategory = await _context.PhotoCategories
                .FirstOrDefaultAsync(m => m.IdCategory == id);
            if (photoCategory == null)
            {
                return NotFound();
            }

            return View(photoCategory);
        }

        // POST: PhotoCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhotoCategories == null)
            {
                return Problem("Entity set 'MGPhotoDbContext.PhotoCategories'  is null.");
            }
            var photoCategory = await _context.PhotoCategories.FindAsync(id);
            if (photoCategory != null)
            {
                _context.PhotoCategories.Remove(photoCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoCategoryExists(int id)
        {
          return (_context.PhotoCategories?.Any(e => e.IdCategory == id)).GetValueOrDefault();
        }
    }
}
