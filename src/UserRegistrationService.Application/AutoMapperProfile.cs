using AutoMapper;
using Common.DTO.Users;
using UserRegistrationService.Application.Users.Commands.CreateUserValidation;

namespace UserRegistrationService.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserValidationCommand, CreateUserDto>();
        }
    }
}