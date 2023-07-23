using HardwareStoreRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.DBContext
{
    public class HardwareStoreDBContext:DbContext  
    {
        public HardwareStoreDBContext()
        {
        }

        public HardwareStoreDBContext(DbContextOptions<HardwareStoreDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Barcodes> Barcodes { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InvoiceItem>().HasKey(ii => new { ii.ProductId, ii.InvoiceId });//composit key
            modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique();}); // unique emails  (not duplicat)

        }
    }
}
