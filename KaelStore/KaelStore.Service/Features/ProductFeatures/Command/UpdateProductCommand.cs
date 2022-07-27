using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.ProductFeatures.Command
{
    public class UpdateProductCommand : IRequest<Response<Product>>
    {
        protected int Id { get; private set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public UpdateProductCommand WithId(int id)
        {
            Id = id;
            return this;
        }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<Product>>
        {
            private readonly IApplicationDbContext _context;
            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var existingProduct = await _context.Products.FindAsync(request.Id);
                if (existingProduct == null)
                {
                    return new Response<Product>("Product tidak tersedia!");
                }
                _context.Products.Attach(existingProduct);

                existingProduct.ProductName = request.ProductName;
                existingProduct.CategoryId = request.CategoryId;
                existingProduct.Price = request.Price;

                await _context.SaveChangesAsync();
                return new Response<Product>(existingProduct);
            }
        }
    }
}
