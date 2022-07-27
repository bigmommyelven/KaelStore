using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.CategoryFeatures.Commands
{
    public class DeleteCategoryByIdCommand : IRequest<Response<Category>>
    {
        protected int Id { get; set; }
        public DeleteCategoryByIdCommand(int id)
        {
            Id = id;
        }
        public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, Response<Category>>
        {
            private readonly IApplicationDbContext _context;
            public DeleteCustomerByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<Category>> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
            {
                var existingCategory = await _context.Categories.Where(c => c.Id == request.Id).FirstOrDefaultAsync();
                if (existingCategory == null)
                {
                    return new Response<Category>("Category tidak ditemukan!");
                }
                _context.Categories.Remove(existingCategory);
                await _context.SaveChangesAsync();
                return new Response<Category>(existingCategory);
            }
        }
    }
}
