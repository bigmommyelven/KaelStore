using System.Collections.Generic;

namespace KaelStore.Service.DTO.Order
{
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public List<OrderItemModel> Items { get; set; }
    }
}
