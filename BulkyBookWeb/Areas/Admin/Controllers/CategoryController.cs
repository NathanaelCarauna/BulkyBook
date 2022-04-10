using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unityOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unityOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unityOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unityOfWork.Category.Add(category);
                _unityOfWork.Save();
                TempData["success"] = "Category created successfuly";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbList = _unityOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbList == null)
            {
                return NotFound();
            }

            return View(categoryFromDbList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unityOfWork.Category.Update(category);
                _unityOfWork.Save();
                TempData["success"] = "Category updated successfuly";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var categoryFromDb = _unityOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null) { return NotFound(); }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            _unityOfWork.Category.Remove(category);
            _unityOfWork.Save();
            TempData["success"] = "Category deleted successfuly";
            return RedirectToAction("Index");
        }
    }

}
