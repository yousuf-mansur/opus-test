using Microsoft.EntityFrameworkCore;
using ProductsManagement.Models;

namespace ProductsManagement.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext>options):base(options)
        {
            
        }
        public virtual DbSet<Product> Products=>Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p=>p.Price)
                .HasPrecision(18, 2);
            });
        }
    }
}
