using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            Product product = new Product();
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select( x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()});
            IEnumerable<SelectListItem> coverTypeList = _unitOfWork.CoverType.GetAll().Select( x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()});
            if(id == null || id == 0)
            {
                //Create
                ViewBag.CategoryList = categoryList;
                ViewBag.CoverTypeList = coverTypeList;
                return View(product);
            }
            else
            {
                //Update
            }
            return View(product);

        }

        [HttpPost]
        public IActionResult Upsert()
        {
            return View();
        }
    }
}
