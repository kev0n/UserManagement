using MediatR;

namespace UserStorageService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}