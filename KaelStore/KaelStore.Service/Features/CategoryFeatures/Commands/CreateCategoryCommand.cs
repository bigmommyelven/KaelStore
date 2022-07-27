using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.CategoryFeatures.Commands
{
    public class CreateCategoryCommand : IRequest<Response<Category>>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<Category>>
        {
            private readonly IApplicationDbContext _context;
            public CreateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Response<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = new Category
                {
                    CategoryName = request.CategoryName,
                    Description = request.Description
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return new Response<Category>(category);
            }
        }
    }
}
