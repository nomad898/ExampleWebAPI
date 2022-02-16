using Example.Business.Core.DTOs.Enums;
using Example.Web.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Web.API.Attributes.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<UserGroupEnum> _groups;

        public AuthorizeAttribute()
        {
            _groups = new UserGroupEnum[] { };
        }

        public AuthorizeAttribute(params UserGroupEnum[] groups)
        {
            _groups = groups ?? new UserGroupEnum[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context
                .ActionDescriptor
                .EndpointMetadata
                .OfType<AllowAnonymousAttribute>()
                .Any();
            if (allowAnonymous)
            {
                return;
            }

            var user = (UserVM)context.HttpContext.Items["User"];
            if (user == null || _groups.Any() && !_groups.Contains((UserGroupEnum)user.UserGroup.Id))
            {
                context.Result = new JsonResult(
                    new {
                        message = "Unauthorized"
                    })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}

