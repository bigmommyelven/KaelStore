using KaelStore.Domain.Entities;
using KaelStore.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KaelStore.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>(ent =>
            {
                ent.HasKey(od => new { od.OrderId, od.ProductId });
            });

            modelBuilder.Entity<ProductStock>(ent =>
            {
                ent.HasKey(ps => ps.ProductId);
            });

            modelBuilder.SeedData();
        }
    }
}
