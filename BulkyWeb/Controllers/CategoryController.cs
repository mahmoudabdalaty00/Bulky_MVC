
using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.Data;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _Repo;

		public CategoryController(ICategoryRepository context)
		{
			_Repo = context;
		}

		public IActionResult Index()
		{
			List<Category> list = _Repo.GetAll().ToList();
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
			     ModelState.AddModelError("Name" , 
			         "The the DisplayOrder Cannot be exactly match the name ");
			 }
		
			if (ModelState.IsValid)
			{
				 _Repo.Add(category);
				_Repo.Save();
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
			var category = _Repo.Get(u => u.Id == Id);

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
			_Repo.Update(category);
				_Repo.Save();
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
			var category = _Repo.Get(u => u.Id == Id);
			if (category == null)
			{
				return NotFound("there is not any Category with this Id ");

			}
			return View(category);
		}


		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int Id)
		{
			var cat = _Repo.Get(u => u.Id == Id);
			if (cat == null)
			{
				return NotFound("there is not any Category with this Id ");

			}
			_Repo.Remove(cat);
			_Repo.Save();			TempData["success"] = "Category Delete Successfully ";
			return RedirectToAction("Index");

	 
		}
	}
}
