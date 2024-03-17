using System.Threading.Tasks;
using AutoMapper;
using Common.DTO.Users;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using UserStorageService.Application.Users.Commands.CreateUser;

namespace UserStorageService.Infrastructure.Consumers
{
    public class CreateUserConsumer : IConsumer<CreateUserDto>
    {
        private readonly ILogger<CreateUserConsumer> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateUserConsumer(
            ILogger<CreateUserConsumer> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreateUserDto> context)
        {
            _logger.LogInformation(
                "Get new CreateUser message. User: {FirstName} {LastName} {MiddleName}, Email: {Email}, Phone:{PhoneNumber}",
                context.Message.FirstName,
                context.Message.LastName,
                context.Message.MiddleName,
                context.Message.Email,
                context.Message.PhoneNumber);

            var createUserCommand = _mapper.Map<CreateUserCommand>(context.Message);

            await _mediator.Send(createUserCommand);
        }


    }
}