using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Web.API.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public UserGroupVM UserGroup { get; set; }

        public AuthenticateResponse(UserVM user, string token)
        {
            Id = user.Id;
            Login = user.Login;
            UserGroup = new UserGroupVM
            {
                Id = user.Id,
                Code = user.UserGroup.Code
            };
            Token = token;
        }
    }
}
