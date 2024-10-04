using Bulky.DataAccess.Repository.IRepository;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM orderVM { get; set; }


        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            orderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.
                Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),

                OrderDetails = _unitOfWork.OrderDetails
                .GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product"),
            };



            return View(orderVM);
        }



        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);


            orderHeaderDb.Name = orderVM.OrderHeader.Name;
            orderHeaderDb.PhoneNumber = orderVM.OrderHeader.PhoneNumber;
            orderHeaderDb.StreetAddress = orderVM.OrderHeader.StreetAddress;
            orderHeaderDb.City = orderVM.OrderHeader.City;
            orderHeaderDb.State = orderVM.OrderHeader.State;
            orderHeaderDb.PostalCode = orderVM.OrderHeader.PostalCode;


            if (!string.IsNullOrEmpty(orderVM.OrderHeader.Carrier))
            {
                orderHeaderDb.Carrier = orderVM.OrderHeader.Carrier;
            }

            if (!string.IsNullOrEmpty(orderVM.OrderHeader.TracingNumber))
            {
                orderHeaderDb.TracingNumber = orderVM.OrderHeader.TracingNumber;
            }

            _unitOfWork.OrderHeader.Update(orderHeaderDb);
            _unitOfWork.Save();

            TempData["success"] = "Order Detail  Update Successfully ";

            return RedirectToAction(nameof(Details), new
            {
                orderId = orderHeaderDb.Id
            });
        }





        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult StartProcessing()
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderVM.OrderHeader.Id, SD.StatusInProcessw);
            _unitOfWork.Save();
            TempData["success"] = "Order Status  Update Successfully ";

            return RedirectToAction(nameof(Details), new
            {
                orderId = orderVM.OrderHeader.Id
            });
        }



        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult ShippingOrder()
        {
            var orderHeaderDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);


            orderHeaderDb.TracingNumber = orderVM.OrderHeader.TracingNumber;
            orderHeaderDb.Carrier = orderVM.OrderHeader.Carrier;
            orderHeaderDb.OrderStatus = orderVM.OrderHeader.OrderStatus;
            orderHeaderDb.ShippingDate = DateTime.Now;

            if (orderHeaderDb.PaymentStatus == SD.PaymentStatusDelayedPayment)
            {
                orderHeaderDb.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }

            _unitOfWork.OrderHeader.Update(orderHeaderDb);
            _unitOfWork.Save();
            _unitOfWork.OrderHeader.UpdateStatus(orderVM.OrderHeader.Id, SD.StatusShipped);
            _unitOfWork.Save();
            TempData["success"] = "Order Shipped Successfully ";

            return RedirectToAction(nameof(Details), new
            {
                orderId = orderVM.OrderHeader.Id
            });
        }


        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult CanclOrder()
        {
            var orderHeaderDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);


            if (orderHeaderDb.PaymentStatus == SD.PaymentStatusApproved)
            {
                var option = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeaderDb.PaymentIntentId,
                };
                var service = new RefundService();
                Refund refund = service.Create(option);
                _unitOfWork.OrderHeader.UpdateStatus(orderHeaderDb.Id, SD.StatusCancelled, SD.StatusRefunded);



            }
            else
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderHeaderDb.Id, SD.StatusCancelled, SD.StatusRefunded);

            }

            _unitOfWork.Save();
            TempData["success"] = "Order Shipped Successfully ";

            return RedirectToAction(nameof(Details), new
            {
                orderId = orderVM.OrderHeader.Id
            });

        }






        [ActionName(nameof(Details))]
        [HttpPost]
        public IActionResult PayNow()
        {


            orderVM.OrderHeader = _unitOfWork.OrderHeader.
                  Get(u => u.Id == orderVM.OrderHeader.Id, includeProperties: "ApplicationUser");

            orderVM.OrderDetails = _unitOfWork.OrderDetails
           .GetAll(u => u.OrderHeaderId == orderVM.OrderHeader.Id, includeProperties: "Product");

                //stripe Logic 
                 var domain = "https://localhost:7102/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"admin/order/PaymentConfirmation?orderHeaderId={orderVM.OrderHeader.Id}",
                CancelUrl = domain + $"admin/order/details?orderId{orderVM.OrderHeader.Id}",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };
            foreach (var item in orderVM.OrderDetails)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), // $20.50 => 2050
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStripePaymentId(orderVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }


        public IActionResult PaymentConfirmation(int orderHeaderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderHeaderId, includeProperties: "ApplicationUser");
            if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
            {
                //this is an order by company
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentId(orderHeaderId, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(orderHeaderId,  orderHeader.OrderStatus, SD.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
            }
           
            return View(orderHeaderId);
        }

        #region APi Calls 


        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaders;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == userId, includeProperties: "ApplicationUser");
            }
            switch (status)
            {
                case "pending":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusPending);
                    break;
                case "inprocess":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusInProcessw);
                    break;
                case "completed":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
                    break;

                default:
                    break;
            }








            return Json(new { data = objOrderHeaders });
        }




        #endregion

    }
}
