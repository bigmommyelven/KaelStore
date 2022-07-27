using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.CategoryFeatures.Queries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }
        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
        {
            private readonly IApplicationDbContext _context;
            public GetCategoryByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.Where(c => c.Id == request.Id).FirstOrDefaultAsync();
                return category;
            }
        }
    }
}
