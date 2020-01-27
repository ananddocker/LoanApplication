using LoanApplication.Core.Models;
using System.Data.Entity;

namespace LoanApplication.Data
{
    public class LoanApplicationDbContext : DbContext
    {
        public LoanApplicationDbContext() : base("LoanApplicationDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LoanApplicationDbContext,Migrations.Configuration>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public DbSet<LoanApplicant> LoanApplications { get; set; }
    }
}
