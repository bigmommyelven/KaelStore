using KaelStore.Domain.Auth;
using KaelStore.Service.DTO.Response;
using System.Threading.Tasks;

namespace KaelStore.Service.Contract
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
    }
}
