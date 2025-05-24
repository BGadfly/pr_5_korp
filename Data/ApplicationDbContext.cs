using Microsoft.EntityFrameworkCore;
using ProductionManagementSystem.Models;

namespace ProductionManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ProductionLine> ProductionLines { get; set; } = default!;
        public DbSet<Material> Materials { get; set; } = default!;
        public DbSet<ProductMaterial> ProductMaterials { get; set; } = default!;
        public DbSet<WorkOrder> WorkOrders { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the composite key for ProductMaterial
            modelBuilder.Entity<ProductMaterial>()
                .HasKey(pm => new { pm.ProductId, pm.MaterialId });

            // Configure the many-to-many relationship
            modelBuilder.Entity<ProductMaterial>()
                .HasOne(pm => pm.Product)
                .WithMany(p => p.ProductMaterials)
                .HasForeignKey(pm => pm.ProductId);

            modelBuilder.Entity<ProductMaterial>()
                .HasOne(pm => pm.Material)
                .WithMany(m => m.ProductMaterials)
                .HasForeignKey(pm => pm.MaterialId);


            // Seed initial data (optional, but helpful for testing)
            modelBuilder.Entity<Material>().HasData(
                new Material { Id = 1, Name = "Steel", Quantity = 1000, UnitOfMeasure = "kg", MinimalStock = 200 },
                new Material { Id = 2, Name = "Plastic", Quantity = 500, UnitOfMeasure = "kg", MinimalStock = 100 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Widget", ProductionTimePerUnit = 30, MinimalStock = 50 }
            );

            // Configure relationships and constraints as needed
            // Explicitly configure the relationship between ProductionLine and WorkOrder
            modelBuilder.Entity<ProductionLine>()
                .HasOne(p => p.CurrentWorkOrder)
                .WithOne()
                .HasForeignKey<ProductionLine>(p => p.CurrentWorkOrderId)
                .OnDelete(DeleteBehavior.SetNull); // Or DeleteBehavior.Restrict if you don't want to allow null


            modelBuilder.Entity<WorkOrder>()
                .HasOne(w => w.ProductionLine)
                .WithMany(p => p.WorkOrders)
                .HasForeignKey(w => w.ProductionLineId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure relationships and constraints as needed
            // Example: Configure the relationship between WorkOrder and ProductionLine
            modelBuilder.Entity<WorkOrder>()
                .HasOne(w => w.ProductionLine)
                .WithMany(p => p.WorkOrders)
                .HasForeignKey(w => w.ProductionLineId)
                .OnDelete(DeleteBehavior.SetNull); // Prevent cascading delete
        }
    }
}
