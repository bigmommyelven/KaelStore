using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.ProductStockFeatures.Command
{
    public class AddProductStockCommand : IRequest<Response<ProductStock>>
    {
        public int Id { get; set; }
        public int Stock { get; set; }

        public class AddProductStockCommandHandler : IRequestHandler<AddProductStockCommand, Response<ProductStock>>
        {
            private readonly IApplicationDbContext _context;
            public AddProductStockCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Response<ProductStock>> Handle(AddProductStockCommand request, CancellationToken cancellationToken)
            {
                var foundEnt = await _context.ProductStocks.FindAsync(request.Id);
                if (foundEnt == null)
                {
                    return new Response<ProductStock>($"Stok dengan ProductId = {request.Id} tidak ditemukan!");
                }

                _context.ProductStocks.Attach(foundEnt);
                foundEnt.Stock = request.Stock;
                _context.ProductStocks.Add(foundEnt);

                return new Response<ProductStock>(foundEnt);
                
            }
        }

    }
}
