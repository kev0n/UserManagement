using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using UserStorageService.Application.Users.Commands.CreateUser;
using Xunit;

namespace UserStorageService.Application.Tests.Users.Commands.CreateUserCommand
{
    public class CreateUserCommandHandlerTests
    {
        private Mock<ILogger<CreateUserCommandHandler>> mockLogger;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IMapper> mockMapper;

        private CreateUserCommandHandler underTest;

        public CreateUserCommandHandlerTests()
        {
            mockLogger = new Mock<ILogger<CreateUserCommandHandler>>();
            mockUserRepository = new Mock<IUserRepository>();
            mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<User>(It.IsAny<Application.Users.Commands.CreateUser.CreateUserCommand>()))
                .Returns(_user);

            underTest = new CreateUserCommandHandler(mockLogger.Object, mockUserRepository.Object, mockMapper.Object);
        }

        private readonly Application.Users.Commands.CreateUser.CreateUserCommand _createUserCommand = new Application.Users.Commands.CreateUser.CreateUserCommand
        {
            Email = "email@domain.com",
            FirstName = "Alex",
            LastName = "Doe",
            PhoneNumber = "+7788885"
        };

        private const int UserId = 11;

        private readonly User _user = new User()
        {
            Id = UserId,
            Email = "email@domain.com",
            FirstName = "Alex",
            LastName = "Doe",
            PhoneNumber = "+7788885"
        };

        [Fact]
        public async Task Handle_Should_Create_NewUser()
        {
            mockUserRepository.Setup(x => x.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(_user);

            var createdId = await underTest.Handle(_createUserCommand, CancellationToken.None);

            Assert.Equal(_user.Id, createdId);
        }
    }
}