using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.CategoryFeatures.Queries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<Category>>
    {
        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCategoryQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Category>> Handle(
                GetAllCategoryQuery request,
                CancellationToken cancellationToken)
            {
                var categoryList = await _context.Categories.ToListAsync();
                if (categoryList == null)
                {
                    return null;
                }

                return categoryList.AsReadOnly();
            }
        }
    }
}
