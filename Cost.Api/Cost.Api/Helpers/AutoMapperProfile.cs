using AutoMapper;
using Cost.Api.Data.Entities;
using Cost.Api.Models;

namespace Cost.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
