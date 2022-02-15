using AutoMapper;
using Example.Business.Core.DTOs;
using Example.Database.EF.Models;

namespace Example.Business.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}
