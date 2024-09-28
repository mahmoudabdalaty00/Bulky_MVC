using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.Models;
using System.Diagnostics;

namespace BulkyWeb.Areas.Customer.Controllers
{

     [Area("Customer")]

    public class HomeController : Controller
    {
   
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            IEnumerable<Product> productList =  _UnitOfWork.Product.GetAll( includeProperties :"Category");
            return View(productList);
        }


        public IActionResult Details( int ProductId)
        {
            if (ProductId == 0 )
            {
                return NotFound("This ${Id} Not Found");
            }
            Product product = _UnitOfWork.Product.Get(p => p.Id == ProductId, includeProperties: "Category");
            return View(product);
        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
