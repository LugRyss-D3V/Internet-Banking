
using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) { }

        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region "Tables & Primary Keys"
            
            builder.Entity<Beneficiary>()
                .ToTable("Beneficiaries")
                .HasKey(b => new { b.BeneficiaryId, b.UserId, b.SavingAccountId });
            
            builder.Entity<Transaction>()
                .ToTable("Transactions")
                .HasKey(t => t.TransactionId);

            builder.Entity<Products>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<SavingAccount>("SavingAccount")
                .HasValue<Loan>("Loan")
                .HasValue<CreditCard>("CreditCard");
            #endregion
           
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
