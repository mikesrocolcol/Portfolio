using Profile.Data;
using Profile.Models;
using Microsoft.AspNetCore.Mvc;

namespace Profile.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly ExpenseDbContext _context;


        public ExpenseCategoryController(ExpenseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<ExpenseCategoryEntity> ExpenseCategories = _context.ExpenseCategory;
            return View(ExpenseCategories);
        }

        //GET
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]

        public IActionResult Create(ExpenseCategoryEntity expenseDetails)
        {

            if (ModelState.IsValid)
            {
                _context.ExpenseCategory.Add(expenseDetails);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult ExpenseCategoryUpdate(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }



            var _ExpenseCatDetails = _context.ExpenseCategory.Find(id);


            if (_ExpenseCatDetails == null)
            {
                return NotFound();
            }

            return View(_ExpenseCatDetails);
        }
        //POST
        [HttpPost]

        public IActionResult Edit(ExpenseCategoryEntity obj)
        {

            if (ModelState.IsValid)
            {
                _context.ExpenseCategory.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //DELETE

        public IActionResult ExpenseCategoryDelete(int? Id)
        {


            var _DelExpenseCatDetails = _context.ExpenseCategory.Find(Id);


            if (_DelExpenseCatDetails == null)
            {
                return NotFound();
            }

            return View(_DelExpenseCatDetails);
        }
        //POST

        public IActionResult Delete(int? ExpenseCategoryId)
        {

            var _DelExpenseDetails = _context.ExpenseCategory.Find(ExpenseCategoryId);
            if (_DelExpenseDetails == null)
            {
                return NotFound();
            }
            _context.ExpenseCategory.Remove(_DelExpenseDetails);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }


    }


}
