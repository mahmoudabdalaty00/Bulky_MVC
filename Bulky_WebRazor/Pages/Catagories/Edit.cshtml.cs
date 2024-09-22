using Bulky_WebRazor.Data;
using Bulky_WebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor.Pages.Catagories
{
	public class EditModel : PageModel
	{
		private readonly AppDbContext _context;
		[BindProperty]
		public Category Category { get; set; }


		public EditModel(AppDbContext context)
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
			if (ModelState.IsValid)
			{
			//	 _context.Categories.Remove(Category);
				_context.Categories.Update(Category);
      
                _context.SaveChanges();
					TempData["success"] = "Category Edited Successfully ";
				return RedirectToPage("Index");


			}
			return Page();
		}


	}
}
