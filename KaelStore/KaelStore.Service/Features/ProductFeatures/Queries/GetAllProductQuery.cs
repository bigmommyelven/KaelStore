using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.ProductFeatures.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Product>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllProductQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var productList = await _context.Products
                    .Include(nameof(Category))
                    .Include(nameof(ProductStock))
                    .ToListAsync();

                return productList.AsReadOnly();
            }
        }
    }
}
