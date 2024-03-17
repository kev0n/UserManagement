using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UserStorageService.Application.Organizations.Commands
{
    public class AttachUserToOrgCommandHandler : IRequestHandler<AttachUserToOrgCommand, bool>
    {
        private readonly ILogger<AttachUserToOrgCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public AttachUserToOrgCommandHandler(
            ILogger<AttachUserToOrgCommandHandler> logger,
            IUserRepository userRepository,
            IOrganizationRepository organizationRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;

        }

        public async Task<bool> Handle(AttachUserToOrgCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.FindByIdAsync(request.UserId);

            if (user == null)
            {
                _logger.LogError("User with id={UserId} not found", request.UserId);
                throw new NotFoundException($"User with id={request.UserId} not found");
            }

            if (user.OrganizationId == request.OrganizationId)
            {
                _logger.LogError("User with id={UserId} already in organization", request.UserId);
                throw new CustomException("User already in this organization");
            }

            var organization = await _organizationRepository.FindByIdAsync(request.OrganizationId);

            if (organization == null)
            {
                _logger.LogError("Organization with id={OrganizationId} not found", request.OrganizationId);
                throw new NotFoundException($"Organization with id={request.UserId} not found");
            }

            user.Organization = organization;
            user.OrganizationId = organization.Id;

            var success = await _userRepository.UpdateAsync(user);

            _logger.LogInformation("User id={UserId} attached to Organization id={OrganizationId}",
                user.Id,
                organization.Id);

            return success;
        }
    }

}