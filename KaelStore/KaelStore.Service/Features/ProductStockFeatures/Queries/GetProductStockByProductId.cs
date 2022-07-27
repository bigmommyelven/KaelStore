using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.ProductStockFeatures.Queries
{
    public class GetProductStockByProductId : IRequest<ProductStock>
    {
        public int ProductId { get; set; }

        public class GetProductStockByProductIdHandler : IRequestHandler<GetProductStockByProductId, ProductStock>
        {
            private readonly IApplicationDbContext _context;
            public GetProductStockByProductIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ProductStock> Handle(GetProductStockByProductId request, CancellationToken cancellationToken)
            {
                var productStock = _context.ProductStocks.Where(ps => ps.ProductId == request.ProductId).FirstOrDefault();

                return productStock;
            }
        }
    }
}
