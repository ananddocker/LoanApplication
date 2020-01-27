using LoanApplication.Core.Models;
using LoanApplication.Data.UoW;
using System;

namespace LoanApplication.Web.Models
{
    public class LoanApplicationDecision
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoanApplicationDecision(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool VerifySalary(LoanApplicant loanApplicant)
        {
            var salary = _unitOfWork.LoanApplications.GetByID(loanApplicant.Id);
            if(loanApplicant.Salary < 65000)
            {
                return false;
            }
            return true;
        }
    }
}