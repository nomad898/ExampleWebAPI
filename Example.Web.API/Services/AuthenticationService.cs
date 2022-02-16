using BCryptNet = BCrypt.Net.BCrypt;
using Example.Web.API.Models;
using Microsoft.Extensions.Options;
using Example.Web.API.Utils;
using Example.Business.Core.Interfaces;
using System;
using System.Threading.Tasks;
using AutoMapper;

namespace Example.Web.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public AuthenticationService(
            IJwtUtils jwtUtils,
            IUserService userService,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            _jwtUtils = jwtUtils;
            _userService = userService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _userService.GetAsync(model.Login);

            //if (user == null || !BCryptNet.Verify(model.Password, user.Password))
            if (user == null || model.Password != user.Password)
            {
                throw new ApplicationException("Username or password is incorrect");
            }

            var userVM = _mapper.Map<UserVM>(user);
            var jwtToken = _jwtUtils.GenerateJwtToken(userVM);

            return new AuthenticateResponse(userVM, jwtToken);
        }
    }
}
