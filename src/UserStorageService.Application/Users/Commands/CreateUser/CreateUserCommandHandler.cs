using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UserStorageService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            ILogger<CreateUserCommandHandler> logger,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            user = await _userRepository.CreateAsync(user);

            _logger.LogInformation("User with id={Id} successfully created. User: {FirstName} {LastName} {MiddleName}, Email: {Email}, Phone:{PhoneNumber}",
                user.Id,
                user.FirstName,
                user.LastName,
                user.MiddleName,
                user.Email,
                user.PhoneNumber);

            return user.Id;
        }
    }
}