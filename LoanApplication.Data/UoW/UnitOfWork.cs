namespace LoanApplication.Data.UoW
{
    using LoanApplication.Core.Models;
    using LoanApplication.Data.Repository;

    public interface IUnitOfWork
    {
        IRepository<LoanApplicant> LoanApplications { get; }      
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {

        private LoanApplicationDbContext _dbContext;
        private Repository<LoanApplicant> _loanApplications;
       
        public UnitOfWork(LoanApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<LoanApplicant> LoanApplications
        {
            get
            {
                return _loanApplications ??
                    (_loanApplications = new Repository<LoanApplicant>(_dbContext));
            }
        }       

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
