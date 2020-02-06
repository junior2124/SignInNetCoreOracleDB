using Microsoft.EntityFrameworkCore;
using SignIn.Models;

namespace SignIn.Data
{
    public class ApplictionDBContext : DbContext
    {
        public DbSet<CompanyAddress> CompanyAddress { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(@"DATA SOURCE=hello:1521/prfd1;PASSWORD=hello;PERSIST SECURITY INFO=True;USER ID=NAII");            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyAddress>(entity =>
            {
                entity.ToTable("COMPANYADDRESS");
                entity.HasKey(e => new { e.COMPANYID, e.MAINADDRESSESID });
            });
        }
    }
}
