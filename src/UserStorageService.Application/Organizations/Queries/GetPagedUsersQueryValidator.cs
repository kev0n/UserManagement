using FluentValidation;

namespace UserStorageService.Application.Organizations.Queries
{
    public class GetPagedUsersQueryValidator : AbstractValidator<GetPagedUsersQuery>
    {
        public GetPagedUsersQueryValidator()
        {
            RuleFor(x => x.OrganizationId)
                .GreaterThan(0);
        }
    }
}