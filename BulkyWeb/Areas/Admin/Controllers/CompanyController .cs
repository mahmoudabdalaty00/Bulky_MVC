using Bulky.DataAccess.Repository.IRepository;

using Microsoft.AspNetCore.Mvc.Rendering;


namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
      
        public CompanyController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
          
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }
        #region  Upsert 
        public IActionResult Upsert(int? id)
        {
            
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
               Company objcompany= _unitOfWork.Company.Get(u => u.Id == id);
                return View(objcompany);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Company companyobj)
        {
            if (ModelState.IsValid)
            {
            
 

                if (companyobj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyobj);
                }
                else
                {
                    _unitOfWork.Company.Update(companyobj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                 
                return View(companyobj);
            }
        }

        #endregion


        #region APi Calls 


        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }
        public IActionResult Delete(int? id)
        {
            var objToDelete = _unitOfWork.Company.Get(p => p.Id == id);
            if(objToDelete == null  )
            {
                return Json(new
                {
                    succes = false , message = "Error While Deleting" ,
                });
            }

            _unitOfWork.Company.Remove(objToDelete);
            _unitOfWork.Save();
          
            return Json(new
            {
                succes = true,
                message = " Deleting Success",
            });
         
        }

        #endregion

    }
}