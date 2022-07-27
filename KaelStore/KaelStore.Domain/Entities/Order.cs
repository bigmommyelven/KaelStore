using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KaelStore.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalBeforeDiscount { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
