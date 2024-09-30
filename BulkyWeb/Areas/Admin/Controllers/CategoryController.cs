
using Bulky.DataAccess.Repository.IRepository;


namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
     
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> list = _unitOfWork.Category.GetAll().ToList();
            return View(list);
        }


        #region Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name",
                    "The the DisplayOrder Cannot be exactly match the name ");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully ";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        #endregion
        public IActionResult Edit(int Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound("there is not any Category with this Id ");
            }
            var category = _unitOfWork.Category.Get(u => u.Id == Id);

            if (category == null)
            {
                return NotFound("there is not any Category with this Id ");

            }
            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited Successfully ";
                return RedirectToAction("Index");
            }
            return View();
        }





        public IActionResult Delete(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound("there is not any Category with this Id ");
            }
            var category = _unitOfWork.Category.Get(u => u.Id == Id);
            if (category == null)
            {
                return NotFound("there is not any Category with this Id ");

            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var cat = _unitOfWork.Category.Get(u => u.Id == Id);
            if (cat == null)
            {
                return NotFound("there is not any Category with this Id ");

            }
            _unitOfWork.Category.Remove(cat);
            _unitOfWork.Save(); TempData["success"] = "Category Delete Successfully ";
            return RedirectToAction("Index");


        }
    }
}
