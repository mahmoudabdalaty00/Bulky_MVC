using Bulky_WebRazor.Data;
using Bulky_WebRazor.Models;
 
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor.Pages.Catagories
{
    public class IndexModel : PageModel
    {

        private readonly AppDbContext _context;
        public List<Category> CategoriesList { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            CategoriesList = _context.Categories.ToList();

        }
    }
}
