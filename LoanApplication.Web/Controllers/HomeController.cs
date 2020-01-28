using FluentValidation;
using LoanApplication.Core.Models;
using LoanApplication.Data.Repository;
using LoanApplication.Data.UoW;
using LoanApplication.Web.Validators;
using System.Web.Mvc;

namespace LoanApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LoanApplicantValidator _loanApplicantValidator;
        
        public HomeController(IUnitOfWork unitOfWork, LoanApplicantValidator loanApplicantValidator)
        {
            _unitOfWork = unitOfWork;
            _loanApplicantValidator = loanApplicantValidator;           
        }

        public ActionResult Index()
        {
            var uservalues = new LoanApplicant { FirstName = "" };
            var valid = _loanApplicantValidator.Validate(uservalues);
            if(valid.Errors.Count == 0)
            {

            }

           var result = _unitOfWork.LoanApplications.GetByID(1);
           return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}