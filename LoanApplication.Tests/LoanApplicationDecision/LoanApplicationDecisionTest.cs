using FluentValidation.TestHelper;
using LoanApplication.Core.Models;
using LoanApplication.Data.Repository;
using LoanApplication.Data.UoW;
using LoanApplication.Web.Controllers;
using LoanApplication.Web.Models;
using LoanApplication.Web.Validators;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace LoanApplication.Tests
{
   [TestFixture]
    public class LoanApplicationDecisionTest
    {
        Mock<IUnitOfWork> mockLoanApplicant;
        Mock<IRepository<LoanApplicant>> repoMockLoan;
        LoanApplicant loanApplicant;
        LoanApplicantValidator validator= new LoanApplicantValidator();

       [OneTimeSetUp]
        public void Initialize()
        {
          mockLoanApplicant=  new Mock<IUnitOfWork>();
            repoMockLoan = new Mock<IRepository<LoanApplicant>>();
          loanApplicant = new LoanApplicant { Id = 1, FirstName = "First Name", LastName = "Last Name", Salary = 65000 };
        }

        [Test]
        public void GetApplicantDetailsToVerifySalary()
        {            
            var loanApplicationDecision = new LoanApplicationDecision(mockLoanApplicant.Object);
            mockLoanApplicant.Setup(_ => _.LoanApplications.GetByID(1)).Returns(new LoanApplicant { Salary = 66000});
            var result= loanApplicationDecision.VerifySalary(loanApplicant);
            Assert.IsTrue(result);
        }

        [Test]
        public void InsertLoanApplicant()
        {
            var result = mockLoanApplicant.Setup(_ => _.LoanApplications.Insert(It.IsAny<LoanApplicant>())).Returns(loanApplicant);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Should_have_error_when_Name_is_null()
        {
            validator.ShouldHaveValidationErrorFor(person => person.FirstName, null as string);
        }

        [Test]
        public void Should_not_have_error_when_name_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(person => person.FirstName, "Jeremy");
        }

        [Test]
        public void Should_not_exceed_salary_max_limit()
        {
            decimal max = decimal.MaxValue-1;
            validator.ShouldHaveValidationErrorFor(person => person.Salary,max+1);
        }

        [Test]
        public void Should_not_exceed_salary_exceed_max_limit_exception()
        {
            decimal max = decimal.MaxValue;
            validator.ShouldHaveValidationErrorFor(person => person.Salary, max);
        }

        [Test]
        public void TestDepartmentIndex()
        {
            var obj = new HomeController(mockLoanApplicant.Object,validator);
            
            mockLoanApplicant.Setup(u => u.LoanApplications).Returns(repoMockLoan.Object);
            mockLoanApplicant.Setup(u => u.LoanApplications.GetByID(1)).Returns(loanApplicant);

            var actResult = obj.Index() as ViewResult;           
            
            Assert.That(actResult.ViewName, Is.EqualTo("Index"));
        }
    }
}
