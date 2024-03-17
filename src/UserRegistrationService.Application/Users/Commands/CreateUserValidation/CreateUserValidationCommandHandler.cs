using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO.Users;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UserRegistrationService.Application.Users.Commands.CreateUserValidation
{
    public class CreateUserValidationCommandHandler : IRequestHandler<CreateUserValidationCommand, bool>
    {
        private readonly ILogger<CreateUserValidationCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CreateUserValidationCommandHandler(
            ILogger<CreateUserValidationCommandHandler> logger,
            IPublishEndpoint publishEndpoint,
            IMapper mapper)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateUserValidationCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = _mapper.Map<CreateUserDto>(request);
            await _publishEndpoint.Publish(user, cancellationToken);

            _logger.LogInformation("Published to queue User: {FirstName} {LastName} {MiddleName}, Email: {Email}, Phone:{PhoneNumber}",
                request.FirstName,
                request.LastName,
                request.MiddleName,
                request.Email,
                request.PhoneNumber
            );

            return true;
        }
    }
}