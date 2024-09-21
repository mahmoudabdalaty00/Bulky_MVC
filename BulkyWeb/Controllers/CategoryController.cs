
namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CategoryController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			List<Category> list = _context.Categories.ToList();
			return View(list);
		}
	
		
		#region
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
				_context.Categories.Add(category);
				_context.SaveChanges();
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
			var category = _context.Categories.Find(Id);
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
				_context.Categories.Update(category);
				_context.SaveChanges();
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
			var category = _context.Categories.Find(Id);
			if (category == null)
			{
				return NotFound("there is not any Category with this Id ");

			}
			return View(category);
		}


		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int Id)
		{
			var cat = _context.Categories.Find(Id);
			if (cat == null)
			{
				return NotFound("there is not any Category with this Id ");

			}
			_context.Categories.Remove(cat);
			_context.SaveChanges();
			TempData["success"] = "Category Delete Successfully ";
			return RedirectToAction("Index");

	 
		}
	}
}
