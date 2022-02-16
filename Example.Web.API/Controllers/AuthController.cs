using Microsoft.AspNetCore.Mvc;
using Example.Web.API.Attributes.Security;
using Example.Web.API.Models;
using Example.Business.Core.DTOs.Enums;
using Example.Web.API.Services;
using System.Threading.Tasks;

namespace Example.Web.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _authenticationService.Authenticate(model);
            return Ok(response);
        }
    }
}