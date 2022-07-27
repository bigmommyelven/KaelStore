using System.Collections.Generic;

namespace KaelStore.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public Category Category { get; set; }
        public ProductStock ProductStock { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
