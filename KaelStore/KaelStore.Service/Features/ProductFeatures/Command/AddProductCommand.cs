using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Response;
using KaelStore.Service.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.ProductFeatures.Command
{
    public class AddProductCommand : IRequest<Response<Product>>
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response<Product>>
        {
            private readonly IApplicationDbContext _context;
            public AddProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Response<Product>> Handle(AddProductCommand request, CancellationToken cancellationToken)
            {
                var existingCategory = _context.Categories.Where(c => c.Id == request.CategoryId).FirstOrDefault();
                if (existingCategory == null)
                {
                    return new Response<Product>("Category tidak tersedia!");
                }

                var existingProduct = _context.Products.Where(p => 
                p.ProductName == request.ProductName && p.CategoryId == request.CategoryId)
                    .FirstOrDefault();

                if (existingProduct != null)
                {
                    return new Response<Product>("Product sudah tersedia!");
                }

                var product = new Product
                {
                    CategoryId = request.CategoryId,
                    Price = request.Price,
                    ProductName = request.ProductName
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return new Response<Product>(product);
            }
        }
    }
}
