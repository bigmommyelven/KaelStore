using System;

namespace KaelStore.Domain.Entities
{
    public class OrderDetail
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
