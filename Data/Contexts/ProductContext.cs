using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<SubcategoryEntity> Subcategories { get; set; }

        public DbSet<ImageEntity> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<ProductEntity>().HasMany(x => x.Images).WithOne().HasForeignKey(x => x.Id);

            modelBuilder.Entity<CategoryEntity>().HasKey(c => c.Id);

            modelBuilder.Entity<CategoryEntity>().HasMany(x => x.Subcategories).WithOne().HasForeignKey(x => x.Id);

            modelBuilder.Entity<SubcategoryEntity>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<ImageEntity>()
                .HasKey(i => i.Id);

        }
    }
}
