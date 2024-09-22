using Bulky_WebRazor.Data;
using Bulky_WebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor.Pages.Catagories
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
         public Category Category { get; set; }

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
         _context.Categories.Add(Category);
            _context.SaveChanges();
            TempData["success"] = "Category Created Successfully ";

            return RedirectToPage("Index");
        }
    }
}
