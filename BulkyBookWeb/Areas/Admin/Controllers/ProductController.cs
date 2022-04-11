using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

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
            if(id == null || id == 0)
            {
                //Create
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
