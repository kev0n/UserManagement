using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using UserStorageService.Application.Organizations.Commands;
using Xunit;

namespace UserStorageService.Application.Tests.Organization.Commands
{
    public class AttachUserToOrgCommandHandlerTests
    {
        private Mock<ILogger<AttachUserToOrgCommandHandler>> mockLogger;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IOrganizationRepository> mockOrganizationRepository;

        private AttachUserToOrgCommandHandler underTest;

        public AttachUserToOrgCommandHandlerTests()
        {
            mockLogger = new Mock<ILogger<AttachUserToOrgCommandHandler>>();
            mockUserRepository = new Mock<IUserRepository>();
            mockOrganizationRepository = new Mock<IOrganizationRepository>();

            underTest = new AttachUserToOrgCommandHandler(mockLogger.Object, mockUserRepository.Object, mockOrganizationRepository.Object);
        }

        private const int UserId = 11;

        private readonly User _user = new User()
        {
            Id = UserId,
            Email = "email@domain.com",
            FirstName = "Alex",
            LastName = "Doe",
            PhoneNumber = "+7788885"
        };

        private const int OrganizationId = 5;

        private readonly Domain.Entities.Organization _organization = new Domain.Entities.Organization
        {
            Id = OrganizationId, Name = "Main Organization"
        };


        [Fact]
        public async Task Handle_Should_AttachUserToOrganization()
        {
            // Arrange
            mockUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_user);
            mockOrganizationRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_organization);
            mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            var command = new AttachUserToOrgCommand() {OrganizationId = OrganizationId, UserId = UserId};

            // Act
            var result = await underTest.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_UserIsNull()
        {
            // Arrange
            mockUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User) null);
            mockOrganizationRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_organization);
            mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            var command = new AttachUserToOrgCommand() {OrganizationId = OrganizationId, UserId = UserId};

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => underTest.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_OrganizationIsNull()
        {
            // Arrange
            mockUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_user);
            mockOrganizationRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Domain.Entities.Organization) null);
            mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            var command = new AttachUserToOrgCommand() {OrganizationId = OrganizationId, UserId = UserId};

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => underTest.Handle(command, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_Should_ThrowException_When_UserAlreadyInOrganization()
        {
            _user.Organization = _organization;
            _user.OrganizationId = OrganizationId;
            // Arrange
            mockUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_user);
            mockOrganizationRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_organization);
            mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            var command = new AttachUserToOrgCommand() {OrganizationId = OrganizationId, UserId = UserId};

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => underTest.Handle(command, CancellationToken.None));
            Assert.Equal("User already in this organization", exception.Message);
        }
    }
}