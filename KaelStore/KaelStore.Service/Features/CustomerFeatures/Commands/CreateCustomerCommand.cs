using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using KaelStore.Service.DTO.Customer;
using KaelStore.Service.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.CustomerFeatures.Commands
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public CreateCustomerCommand(CreateCustomerModel request)
        {
            RequestData = request;
        }

        public CreateCustomerModel RequestData { get; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
        {
            private readonly IApplicationDbContext _context;
            public CreateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = new Customer();
                ObjectCopy.CopyProperties(request.RequestData, customer);

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return customer;
            }
        }
    }
}
