using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{

    [Area("Customer")]

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var  claim = claimIdentity.FindFirst(
                                    ClaimTypes.NameIdentifier);


            if(claim != null )
            {
                var userId = claim;
                HttpContext.Session.SetInt32(SD.SessionCart,
                 _UnitOfWork.ShoppingCart.GetAll(
                          c => c.ApplicationUserId == userId.Value).Count()
                 );
            }
            IEnumerable<Product> productList = _UnitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }


        public IActionResult Details(int ProductId)
        {
            if (ProductId == 0)
            {
                return NotFound("This ${Id} Not Found");
            }

            ShoppingCart cart = new()
            {
                Product = _UnitOfWork.Product.Get(p => p.Id == ProductId, includeProperties: "Category"),
                Count = 1,
                ProductId = ProductId
            };


            return View(cart);

        }


        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(
                                    ClaimTypes.NameIdentifier).Value;

            cart.ApplicationUserId = userId;

            ShoppingCart CartDb = _UnitOfWork.ShoppingCart.Get(
                             c => c.ApplicationUserId == userId &&
                             c.ProductId == cart.ProductId
                             );


            if (CartDb != null)
            {
                //cart exist 
                CartDb.Count += cart.Count;
                _UnitOfWork.ShoppingCart.Update(CartDb);

                _UnitOfWork.Save();

            }
            else
            {
                //add new cart 
                _UnitOfWork.ShoppingCart.Add(cart);


                _UnitOfWork.Save();
                //add session to my cart 
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _UnitOfWork.ShoppingCart.GetAll(
                             c => c.ApplicationUserId == userId).Count()
                    );
            }

            TempData["success"] = "Cart Updated Successfully ";





            return RedirectToAction(nameof(Index));
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
