using Microsoft.EntityFrameworkCore;
using ShoppingCartSANA.Domain.Entities;

namespace ShoppingCartSANA.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas principales
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductsCategories> ProductsCategories { get; set; }

        // Tablas adicionales (para procesar pedidos)
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =======================
            // ProductsCategories (tabla pivote)
            // =======================
            modelBuilder.Entity<ProductsCategories>()
                .HasKey(pc => new { pc.Product_Id, pc.Category_Id });

            modelBuilder.Entity<ProductsCategories>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductsCategories)
                .HasForeignKey(pc => pc.Product_Id);

            modelBuilder.Entity<ProductsCategories>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductsCategories)
                .HasForeignKey(pc => pc.Category_Id);

            // Orders
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id_Order);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.Customer_Id);

            // OrderDetails
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.Id);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.Order_Id);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.Product_Id);

            // =======================
            // Customers
            // =======================            
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);
                      
            
        }
    }
}
