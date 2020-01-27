using LoanApplication.Core.Models;
using LoanApplication.Data.UoW;
using LoanApplication.Web.Models;
using Moq;
using NUnit.Framework;

namespace LoanApplication.Tests
{
   [TestFixture]
    public class LoanApplicationDecisionTest
    {
        [Test]
        public void GetApplicantDetailsToVerifySalary()
        {
            var mockLoanApplicant = new Mock<IUnitOfWork>();
            var loanApplicant = new LoanApplicant { Id= 1, FirstName= "First Name",LastName= "Last Name", Salary= 65000 };
            var loanApplicationDecision = new LoanApplicationDecision(mockLoanApplicant.Object);
            mockLoanApplicant.Setup(_ => _.LoanApplications.GetByID(1)).Returns(new LoanApplicant { Salary = 66000});
            var result= loanApplicationDecision.VerifySalary(loanApplicant);
            Assert.IsTrue(result);
        }
    }
}
