using Example.Web.API.Models;
using System.Threading.Tasks;

namespace Example.Web.API.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}
