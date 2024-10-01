using Bulky.DataAccess.Repository.IRepository;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unit;
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(
                                    ClaimTypes.NameIdentifier).Value;


            ShoppingCartVM = new()
            {
                ShoppingCartList = _unit.ShoppingCart
                        .GetAll(c => c.ApplicationUserId == userId, includeProperties: "Product"),
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetCartPriceBasedonQuantity(cart);
                ShoppingCartVM.OrderTotal += (cart.Price * cart.Count);

            }
            return View(ShoppingCartVM);
        }


        public IActionResult Summery()
        {
            return View();

        }


        public IActionResult Plus(int cartId)
        {

            var cartDb = _unit.ShoppingCart.Get(c => c.Id == cartId);
            cartDb.Count += 1;

            _unit.ShoppingCart.Update(cartDb);
            _unit.Save();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult minus(int cartId)
        {

            var cartDb = _unit.ShoppingCart.Get(c => c.Id == cartId);
            if (cartDb.Count <= 1)
            {

                _unit.ShoppingCart.Remove(cartDb);
            }
            else
            {
                cartDb.Count -= 1;

                _unit.ShoppingCart.Update(cartDb);

            }
            _unit.Save();

            return RedirectToAction(nameof(Index));

        }


        public IActionResult Remove(int cartId)
        {

            var cartDb = _unit.ShoppingCart.Get(c => c.Id == cartId);


            _unit.ShoppingCart.Remove(cartDb);
            _unit.Save();
            return RedirectToAction(nameof(Index));

        }



        private double GetCartPriceBasedonQuantity(ShoppingCart cart)
        {
            if (cart.Count <= 50)
            {
                return cart.Product.Price;
            }
            else
            {
                if (cart.Count <= 100)
                {
                    return cart.Product.Price50;
                }
                else
                {
                    return cart.Product.Price100;
                }
            }


        }





    }
}
