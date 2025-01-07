using Microsoft.EntityFrameworkCore;
using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.Data
{
    public class ApplicationDbContext : DbContext
    {
       

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>()
        //        .HasKey(c => c.CategoryId);

        //    modelBuilder.Entity<Product>()
        //        .HasKey(p => p.ProductId);

        //    modelBuilder.Entity<Product>()
        //        .HasOne(p => p.Category)
        //        .WithMany(c => c.Products)
        //        .HasForeignKey(p => p.CategoryId);
        //}
    }
}
