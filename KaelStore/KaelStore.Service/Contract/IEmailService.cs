using KaelStore.Domain.Settings;
using System.Threading.Tasks;

namespace KaelStore.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
