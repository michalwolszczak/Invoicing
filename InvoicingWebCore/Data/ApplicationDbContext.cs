using InvoicingWebCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoicingWebCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {                
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new Seeder(modelBuilder).Seed();
            //modelBuilder.Entity<Contractor>()
            //    .HasOne(a => a.Address)
            //    .WithOne(c => c.Contractor)
            //    .HasForeignKey<Address>(a => a.ContractorForeignKey);
        }
    }  
}
