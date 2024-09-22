using Bulky_WebRazor.Data;
using Bulky_WebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor.Pages.Catagories
{
	public class DeleteModel : PageModel
	{
		private readonly AppDbContext _context;

		[BindProperty]
		public Category? Category { get; set; }

		public DeleteModel(AppDbContext context)
		{
			_context = context;
		}

		public void OnGet(int? Id)
		{
			if (Id != null || Id != 0)
			{
				Category = _context.Categories.Find(Id);
			}


		}

		public IActionResult OnPost()
		{
			Category? category = _context.Categories.Find(Category.Id);
			if (category == null)
			{
				return Page();
			}
			_context.Categories.Remove(category);
			
			_context.SaveChanges();
			TempData["success"] = "Category Deleted Successfully ";
			return RedirectToPage("Index");


		}
	}
}
