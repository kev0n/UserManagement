using FluentValidation;

namespace UserRegistrationService.Application.Users.Commands.CreateUserValidation
{
    public class CreateUserValidationCommandValidator : AbstractValidator<CreateUserValidationCommand>
    {
        public CreateUserValidationCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();
        }
    }
}