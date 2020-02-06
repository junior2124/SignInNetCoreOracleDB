using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using SignIn.Models;
using System.Data;

namespace SignIn.Data
{
    public class ApplictionDBContext : DbContext
    {
        public DbSet<CompanyAddress> CompanyAddress { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(@"DATA SOURCE=odapri1:1521/prfd;PASSWORD=testfirst;PERSIST SECURITY INFO=True;USER ID=NAII");
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
