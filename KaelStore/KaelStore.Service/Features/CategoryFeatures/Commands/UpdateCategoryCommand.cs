using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.CategoryFeatures.Commands
{
    public class UpdateCategoryCommand : IRequest<Response<Category>>
    {
        protected int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public UpdateCategoryCommand WithId(int id)
        {
            Id = id;
            return this;
        }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<Category>>
        {
            private readonly IApplicationDbContext _context;
            public UpdateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var existingCategory = await _context.Categories.FindAsync(request.Id);
                if (existingCategory == null)
                {
                    return new Response<Category>("Category tidak tersedia!");
                }
                _context.Categories.Attach(existingCategory);

                existingCategory.CategoryName = request.CategoryName;
                existingCategory.Description = request.Description;

                await _context.SaveChangesAsync();
                return new Response<Category>(existingCategory);
            }
        }
    }
}
