using MediatR;

namespace UserRegistrationService.Application.Users.Commands.CreateUserValidation
{
    public class CreateUserValidationCommand : IRequest<bool>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}