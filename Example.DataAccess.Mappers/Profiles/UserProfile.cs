using AutoMapper;
using Example.Business.Core.DTOs;
using Example.Database.EF.Models;

namespace Example.DataAccess.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
