using AutoMapper;
using Common.DTO.Users;
using Domain.Entities;
using UserStorageService.Application.Users.Commands.CreateUser;

namespace UserStorageService.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserDto, CreateUserCommand>();
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}