using MediatR;

namespace UserStorageService.Application.Organizations.Commands
{
    public class AttachUserToOrgCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
    }
}