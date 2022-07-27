using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Response;
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
    public class GetProductByIdQuery : IRequest<Response<Product>>
    {
        protected int Id { get; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<Product>>
        {
            private readonly IApplicationDbContext _context;
            public GetProductByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var existingProduct = _context.Products.Where(p => p.Id == request.Id).FirstOrDefault();
                if (existingProduct == null)
                {
                    return new Response<Product>("Product tidak ditemukan!");
                }
                return new Response<Product>(existingProduct);
            }
        }
    }
}
