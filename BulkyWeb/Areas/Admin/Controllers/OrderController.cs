using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
                Get(u => u.Id == orderId , includeProperties:"ApplicationUser"),

                OrderDetails = _unitOfWork.OrderDetails
                .GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product"),
            };



            return View(orderVM);
        }



        [HttpPost]
        [Authorize(Roles = SD.Role_Admin+","+SD.Role_Employee)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);


            orderHeaderDb.Name = orderVM.OrderHeader.Name;
            orderHeaderDb.PhoneNumber = orderVM.OrderHeader.PhoneNumber;
            orderHeaderDb.StreetAddress = orderVM.OrderHeader.StreetAddress;
            orderHeaderDb.City = orderVM.OrderHeader.City;
            orderHeaderDb.State = orderVM.OrderHeader.State;
            orderHeaderDb.PostalCode = orderVM.OrderHeader.PostalCode;


            if(!string.IsNullOrEmpty(orderVM.OrderHeader.Carrier))
            {
                orderHeaderDb.Carrier = orderVM.OrderHeader.Carrier;
            }

            if(!string.IsNullOrEmpty(orderVM.OrderHeader.TracingNumber))
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










        #region APi Calls 


        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaders;

            if(User.IsInRole(SD.Role_Admin )|| User.IsInRole(SD.Role_Employee))
            {
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            }
            else
            {
                var claimsIdentity  = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId ==userId , includeProperties:"ApplicationUser");
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
