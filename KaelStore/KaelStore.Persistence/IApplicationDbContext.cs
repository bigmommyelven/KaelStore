using KaelStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KaelStore.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductStock> ProductStocks { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }

        Task<int> SaveChangesAsync();
    }
}
