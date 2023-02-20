using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MGPhoto.Models;
using MGPhoto.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MGPhoto.Controllers
{
    public class GalleryPhotoesController : Controller
    {
        private readonly MGPhotoDbContext _context;
        private readonly IWebHostEnvironment? _environment;

        public GalleryPhotoesController(MGPhotoDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: GalleryPhotoes
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
              return _context.GalleryPhotos != null ? 
                          View(await _context.GalleryPhotos.ToListAsync()) :
                          Problem("Entity set 'MGPhotoDbContext.GalleryPhotos'  is null.");
        }
        
        public async Task<IActionResult> Gallery()
        {
            return _context.GalleryPhotos != null ?
                        View(await _context.GalleryPhotos.ToListAsync()) :
                        Problem("Entity set 'MGPhotoDbContext.GalleryPhotos'  is null.");
        }

        // GET: GalleryPhotoes/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GalleryPhotos == null)
            {
                return NotFound();
            }

            var galleryPhoto = await _context.GalleryPhotos.FirstOrDefaultAsync(m => m.IdPhoto == id);
            var galleryViewModel = new GalleryViewModel()
            {
                Id = galleryPhoto.IdPhoto,
                UploadDate = galleryPhoto.UploadDate,
                Category = galleryPhoto.Category,
                ExistingImage = galleryPhoto.Picture
            };
            if (galleryPhoto == null)
            {
                return NotFound();
            }

            return View(galleryPhoto);
        }

        // GET: GalleryPhotoes/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GalleryPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(GalleryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                GalleryPhoto galleryPhoto = new()
                {
                    UploadDate = model.UploadDate,
                    Category = model.Category,
                    Picture = uniqueFileName
                };

                _context.Add(galleryPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: GalleryPhotoes/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GalleryPhotos == null)
            {
                return NotFound();
            }

            var galleryPhoto = await _context.GalleryPhotos.FindAsync(id);
            var galleryViewModel = new GalleryViewModel()
            {
                Id = galleryPhoto.IdPhoto,
                UploadDate = galleryPhoto.UploadDate,
                Category = galleryPhoto.Category,
                ExistingImage = galleryPhoto.Picture
            };
            if (galleryPhoto == null)
            {
                return NotFound();
            }
            return View(galleryViewModel);
        }

        // POST: GalleryPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, GalleryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var galleryPhoto = await _context.GalleryPhotos.FindAsync(model.Id);
                galleryPhoto.Category = model.Category;
                galleryPhoto.UploadDate = model.UploadDate;
                

                if (model.GalleryPicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "Gallery", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    galleryPhoto.Picture = ProcessUploadedFile(model);
                }
                _context.Update(galleryPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: GalleryPhotoes/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GalleryPhotos == null)
            {
                return NotFound();
            }

            var galleryPhoto = await _context.GalleryPhotos.FirstOrDefaultAsync(m => m.IdPhoto == id);
            var galleryViewModel = new GalleryViewModel()
            {
                Id = galleryPhoto.IdPhoto,
                UploadDate = galleryPhoto.UploadDate,
                Category = galleryPhoto.Category,
                ExistingImage = galleryPhoto.Picture
            };
            if (galleryPhoto == null)
            {
                return NotFound();
            }

            return View(galleryViewModel);
        }

        // POST: GalleryPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GalleryPhotos == null)
            {
                return Problem("Entity set 'MGPhotoDbContext.GalleryPhotos'  is null.");
            }
            var galleryPhoto = await _context.GalleryPhotos.FindAsync(id);
            string deleteFileFromFolder = Path.Combine(_environment.WebRootPath, "Gallery");
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFileFromFolder, galleryPhoto.Picture);
            _context.GalleryPhotos.Remove(galleryPhoto);

            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryPhotoExists(int id)
        {
          return (_context.GalleryPhotos?.Any(e => e.IdPhoto == id)).GetValueOrDefault();
        }




        private string ProcessUploadedFile(GalleryViewModel model)
        {
            string uniqueFileName = null;
            string path = Path.Combine(_environment.WebRootPath, "Gallery");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (model.GalleryPicture != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Gallery");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.GalleryPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.GalleryPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
