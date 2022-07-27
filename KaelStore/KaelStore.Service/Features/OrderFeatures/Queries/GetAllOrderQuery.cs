using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.OrderFeatures.Queries
{
    public class GetAllOrderQuery : IRequest<Response<IEnumerable<Order>>>
    {
        public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, Response<IEnumerable<Order>>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllOrderQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Response<IEnumerable<Order>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
            {
                var orderList = await _context.Orders
                    .Include("OrderDetails")
                    .ToListAsync();

                return new Response<IEnumerable<Order>>(orderList);
            }
        }
    }
}
