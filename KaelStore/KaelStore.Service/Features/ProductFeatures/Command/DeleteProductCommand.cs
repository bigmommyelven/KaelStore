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
    public class DeleteProductCommand : IRequest<Response<Product>>
    {
        protected int Id { get; set; }
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<Product>>
        {
            private readonly IApplicationDbContext _context;
            public DeleteProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<Product>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var existingProduct = await _context.Products.FindAsync(request.Id);
                if (existingProduct == null)
                {
                    return new Response<Product>("Product tidak tersedia!");
                }
                _context.Products.Attach(existingProduct);
                _context.Products.Remove(existingProduct);

                await _context.SaveChangesAsync();
                return new Response<Product>(existingProduct);

            }
        }
    }
}
