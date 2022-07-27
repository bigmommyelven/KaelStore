using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Order;
using KaelStore.Service.DTO.Response;
using KaelStore.Service.Rules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.OrderFeatures.Commands
{
    public class CreateOrderCommand : IRequest<Response<Order>>
    {
        public CreateOrderCommand(CreateOrderModel request)
        {
            RequestData = request;
        }

        public CreateOrderModel RequestData { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<Order>>
        {
            private readonly IApplicationDbContext _context;
            public CreateOrderCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Response<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var rule = new PromotionRule();
                var order = new Order
                {
                    CustomerId = request.RequestData.CustomerId,
                    Id = Guid.NewGuid()
                };

                var orderDetails = new List<OrderDetail>();

                var itemsByProduct = request.RequestData.Items.GroupBy(item => item.ProductId);

                foreach (var g in itemsByProduct)
                {
                    foreach (var item in g)
                    {
                        var product = _context.Products.Find(g.Key);
                        var productStock = _context.ProductStocks.Find(g.Key);
                        _context.Products.Attach(product);
                        _context.ProductStocks.Attach(productStock);

                        if (productStock.Stock - item.Quantity < 0)
                        {
                            return new Response<Order>("Stok barang kurang dari jumlah pembelian");
                        }

                        productStock.Stock -= item.Quantity;

                        var itemTotalPrice = product.Price * item.Quantity;
                        order.TotalBeforeDiscount += itemTotalPrice;
                        orderDetails.Add(new OrderDetail
                        {
                            OrderId = order.Id,
                            Order = order,
                            ProductId = product.Id,
                            Product = product,
                            Quantity = item.Quantity,
                            Total = itemTotalPrice,
                        });
                    }
                }

                var grouppedOrderDetails = orderDetails.GroupBy(od => od.Product.CategoryId)
                    .Select(g => new
                    {
                        Id = g.Key,
                        TotalItems = g.Sum(od => od.Quantity),
                        TotalPrice = g.Sum(od => od.Product.Price * od.Quantity)
                    });

                foreach(var i in grouppedOrderDetails)
                {
                    if (i.Id == 1 && i.TotalItems >= 10)
                    {
                        // Discount 10%
                        order.Discount += i.TotalPrice * (decimal)0.1;
                    }
                }
                if (order.Discount > 20000) order.Discount = 20000;
                // Gk jadi pake RulesEngine
                //order.Discount = await rule.Check(orderDetails);
                order.Total = order.TotalBeforeDiscount - order.Discount;
                order.OrderDetails = orderDetails;

                _context.Orders.Add(order);
                _context.OrderDetails.AddRange(orderDetails);
                await _context.SaveChangesAsync();

                return new Response<Order>(order);
            }
        }
    }
}
