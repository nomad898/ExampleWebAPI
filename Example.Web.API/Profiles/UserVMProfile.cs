using AutoMapper;
using Example.Business.Core.DTOs;
using Example.Web.API.Models;

namespace Example.Web.API.Profiles
{
    public class UserVMProfile : Profile
    {
        public UserVMProfile()
        {
            CreateMap<UserDTO, UserVM>()
                .ReverseMap();
        }
    }
}
