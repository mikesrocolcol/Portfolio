using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models;


namespace Profile.Controllers
{
    public class ImageEntitiesController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ExpenseDbContext _context;
   

        public ImageEntitiesController(ExpenseDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _hostEnvironment = webHost;
        }

        // GET: ImageEntities
        public async Task<IActionResult> Index()
        {
              return _context.ImgCategories != null ? 
                          View(await _context.ImgCategories.ToListAsync()) :
                          Problem("Entity set 'ExpenseDbContext.ImageCategories'  is null.");
        }




        // GET:  ImageEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageFile,FormFile")] ImageEntity imageEntity)
        {
            if (ModelState.IsValid)
            {
                /**//*Save images to wwwroot*//**/
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(imageEntity.FormFile.FileName);
                string extension = Path.GetExtension(imageEntity.FormFile.FileName);
                imageEntity.ImageFile = fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageEntity.FormFile.CopyToAsync(fileStream);
                }




                _context.Add(imageEntity);
                await _context.SaveChangesAsync();
                TempData["success"] = "Created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(imageEntity);
        }



        // GET:  ImageEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ImgCategories == null)
            {
                return NotFound();
            }

            var imageEntity = await _context.ImgCategories.FindAsync(id);
            if (imageEntity == null)
            {
                return NotFound();
            }
            return View(imageEntity);
        }

        // POST:  ImageEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageFile,FormFile")] ImageEntity imageEntity)
        {
            if (id != imageEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {


                



                /**//*Save images to wwwroot*//**/
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(imageEntity.FormFile.FileName);
                string extension = Path.GetExtension(imageEntity.FormFile.FileName);
                imageEntity.ImageFile = fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageEntity.FormFile.CopyToAsync(fileStream);
                }




                _context.Update(imageEntity);
                await _context.SaveChangesAsync();



                try
                {
                    _context.Update(imageEntity);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageEntityExists(imageEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = " Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(imageEntity);
        }

        // GET:  ImageEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ImgCategories == null)
            {
                return NotFound();
            }

            var imageEntity = await _context.ImgCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageEntity == null)
            {
                return NotFound();
            }

            return View(imageEntity);
        }

        // POST:  ImageEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ImgCategories == null)
            {
                return Problem("Entity set 'InventoryContext.Inventories'  is null.");
            }
            var imageEntity = await _context.ImgCategories.FindAsync(id);
            if (imageEntity != null)
            {
                _context.ImgCategories.Remove(imageEntity);
            }
            
            await _context.SaveChangesAsync();
            TempData["success"] = "Deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool ImageEntityExists(int id)
        {
          return (_context.ImgCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        


    }
}
