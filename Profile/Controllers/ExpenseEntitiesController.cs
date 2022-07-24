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
    public class ExpenseEntitiesController : Controller
    {
        private readonly ExpenseDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ExpenseEntitiesController(ExpenseDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _hostEnvironment = webHost;
        }

        // GET: ExpenseEntities
        public async Task<IActionResult> Index()
        {
            var expenseDbContext = _context.Categories.Include(e => e.ExpenseCategory);
            return View(await expenseDbContext.ToListAsync());
        }

        // GET: ExpenseEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var expenseEntity = await _context.Categories
                .Include(e => e.ExpenseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseEntity == null)
            {
                return NotFound();
            }

            return View(expenseEntity);
        }

        // GET: ExpenseEntities/Create
        public IActionResult Create()
        {
            ViewData["ExpenseCategoryId"] = new SelectList(_context.ExpenseCategory, "ExpenseCategoryId", "ExpenseCategoryName");
            return View();
        }

        // POST: ExpenseEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Createdate,Description,ExpenseCategoryId,Image,FormFile")] ExpenseEntity expenseEntity)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(expenseEntity.FormFile.FileName);
                string extension = Path.GetExtension(expenseEntity.FormFile.FileName);
                expenseEntity.Image = fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await expenseEntity.FormFile.CopyToAsync(fileStream);
                }





                _context.Add(expenseEntity);
                await _context.SaveChangesAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpenseCategoryId"] = new SelectList(_context.ExpenseCategory, "ExpenseCategoryId", "ExpenseCategoryName", expenseEntity.ExpenseCategoryId);
            return View(expenseEntity);
        }

        // GET: ExpenseEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {



            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var expenseEntity = await _context.Categories.FindAsync(id);
            if (expenseEntity == null)
            {
                return NotFound();
            }
            ViewData["ExpenseCategoryId"] = new SelectList(_context.ExpenseCategory, "ExpenseCategoryId", "ExpenseCategoryName", expenseEntity.ExpenseCategoryId);

            return View(expenseEntity);
        }

        // POST: ExpenseEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Createdate,Description,ExpenseCategoryId,Image,FormFile")] ExpenseEntity expenseEntity)
        {

            if (id != expenseEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;



                string fileNames = Path.GetFileNameWithoutExtension(expenseEntity.FormFile.FileName);
                string extension = Path.GetExtension(expenseEntity.FormFile.FileName);
                expenseEntity.Image = fileNames = fileNames + extension;
                string path = Path.Combine(wwwRootPath + "/images/", fileNames);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await expenseEntity.FormFile.CopyToAsync(fileStream);
                }



                _context.Update(expenseEntity);
                await _context.SaveChangesAsync();


                try
                {
                    _context.Update(expenseEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseEntityExists(expenseEntity.Id))
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
            ViewData["ExpenseCategoryId"] = new SelectList(_context.ExpenseCategory, "ExpenseCategoryId", "ExpenseCategoryName", expenseEntity.ExpenseCategoryId);
            return View(expenseEntity);
        }

        // GET: ExpenseEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var expenseEntity = await _context.Categories
                .Include(e => e.ExpenseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseEntity == null)
            {
                return NotFound();
            }


            return View(expenseEntity);
        }

        // POST: ExpenseEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ExpenseDbContext.Categories'  is null.");
            }
            var expenseEntity = await _context.Categories.FindAsync(id);
            /*
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", expenseEntity.Image);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);*/

            if (expenseEntity != null)
            {


                _context.Categories.Remove(expenseEntity);
            }
            _context.Categories.Remove(expenseEntity);

            await _context.SaveChangesAsync();
            TempData["success"] = " Deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseEntityExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
