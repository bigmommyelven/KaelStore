using KaelStore.Domain.Entities;
using KaelStore.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KaelStore.Service.Features.CustomerFeatures.Commands
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        public UpdateCustomerCommand(Customer request)
        {
            RequestData = request;
        }

        public Customer RequestData { get; }
        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
        {
            private readonly IApplicationDbContext _context;
            public UpdateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = _context.Customers.Where(a => a.Id == request.RequestData.Id).FirstOrDefault();

                if (customer != null)
                {
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                }

                return customer;
            }
        }
    }
}
