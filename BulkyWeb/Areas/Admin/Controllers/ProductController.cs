using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> list = _unitOfWork.Product.GetAll().ToList();
            return View(list);
        }


        #region Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Product product)
        {

            if (product.Title == product.Price.ToString())
            {
                ModelState.AddModelError("Name",
                    "The the DisplayOrder Cannot be exactly match the name ");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully ";
                return RedirectToAction("Index");
            }
            return View(product);
        }

		#endregion


		#region edit
		public IActionResult Edit(int Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound("there is not any Category with this Id ");
            }
            var product = _unitOfWork.Product.Get(u => u.Id == Id);

            if (product == null)
            {
                return NotFound("there is not any Category with this Id ");

            }
            return View(product);
        }


        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }


		#endregion

		#region delete
		public IActionResult Delete(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound("there is not any Category with this Id ");
            }
            var product = _unitOfWork.Product.Get(u => u.Id == Id);
            if (product == null)
            {
                return NotFound("there is not any Category with this Id ");

            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var pro = _unitOfWork.Product.Get(u => u.Id == Id);
            if (pro == null)
            {
                return NotFound("there is not any Category with this Id ");

            }
            _unitOfWork.Product.Remove(pro);
            _unitOfWork.Save(); TempData["success"] = "Category Delete Successfully ";
            return RedirectToAction("Index");

			#endregion
		}
	}
}
