using Bulky.DataAccess.Repository.IRepository;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unit;
		[BindProperty]
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
				OrderHeader = new()
				{

				}
			};

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetCartPriceBasedonQuantity(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);

			}
			return View(ShoppingCartVM);
		}


		public IActionResult Summery()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(
									ClaimTypes.NameIdentifier).Value;


			ShoppingCartVM = new()
			{
				ShoppingCartList = _unit.ShoppingCart
						.GetAll(c => c.ApplicationUserId == userId, includeProperties: "Product"),
				OrderHeader = new()

			};
			ShoppingCartVM.OrderHeader.ApplicationUser = _unit.ApplicationUser.Get(u => u.Id == userId);

			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
			ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;





			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetCartPriceBasedonQuantity(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);

			}
			return View(ShoppingCartVM);
		}

		[HttpPost]
		[ActionName("Summery")]
		public IActionResult SummeryPost()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(
									ClaimTypes.NameIdentifier).Value;


			ShoppingCartVM.ShoppingCartList = _unit.ShoppingCart
						.GetAll(c => c.ApplicationUserId == userId, includeProperties: "Product");


			ShoppingCartVM.OrderHeader.ApplicationUserId = userId;
			ShoppingCartVM.OrderHeader.PaymentDate = DateTime.Now;

			 ApplicationUser  apps= _unit.ApplicationUser.Get(u => u.Id == userId);

			//ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
			//ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			//ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			//ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			//ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			//ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetCartPriceBasedonQuantity(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);

			}

			if (apps.CompanyId.GetValueOrDefault() == 0)
			{
				//reguler customer 
				ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
				ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;


			}
			else
			{
				//company user 

				ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;

			}


			_unit.OrderHeader.Add(ShoppingCartVM.OrderHeader);
			_unit.Save();


			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				OrderDetails orderDetails = new OrderDetails()
				{
					ProductId = cart.ProductId,
					OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
					Price = cart.Price,
					Count = cart.Count,

				};
				_unit.OrderDetails.Add(orderDetails);
				_unit.Save();
			}




			if (apps.CompanyId.GetValueOrDefault() == 0)
			{
				//reguler customer 
				 


			}

			return RedirectToAction(nameof(OrderConfirmation) , new
			{
				id = ShoppingCartVM.OrderHeader.Id,
				
			});
		}


		public IActionResult OrderConfirmation (int id )
		{
			return View(id);
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
