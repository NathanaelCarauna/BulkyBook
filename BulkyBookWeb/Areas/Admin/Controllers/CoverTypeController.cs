using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unityOfWork;

        public CoverTypeController(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = _unityOfWork.CoverType.GetAll();
            return View(coverTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unityOfWork.CoverType.Add(coverType);
                _unityOfWork.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverType = _unityOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unityOfWork.CoverType.Update(coverType);
                TempData["success"] = "Cover Type updated successfully";
                _unityOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var coverTypeFromDb = _unityOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (coverTypeFromDb == null) return NotFound();
            return View(coverTypeFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType coverType)
        {
            _unityOfWork.CoverType.Remove(coverType);
            _unityOfWork.Save();
            TempData["success"] = "Cover Type removed successfully";
            return RedirectToAction("Index");
        }
    }
}
