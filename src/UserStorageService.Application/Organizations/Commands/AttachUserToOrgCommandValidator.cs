using FluentValidation;

namespace UserStorageService.Application.Organizations.Commands
{
    public class AttachUserToOrgCommandValidator : AbstractValidator<AttachUserToOrgCommand>
    {
        public AttachUserToOrgCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.OrganizationId)
                .GreaterThan(0);
        }
    }
}