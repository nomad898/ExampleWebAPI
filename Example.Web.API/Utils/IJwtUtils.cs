using Example.Web.API.Models;

namespace Example.Web.API.Utils
{
    public interface IJwtUtils
    {
        string GenerateJwtToken(UserVM userVM);
        int? ValidateJwtToken(string token);
    }
}
