using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO.Users;
using Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using UserStorageService.Application.Organizations.Queries;
using Xunit;

namespace UserStorageService.Application.Tests.Organization.Queries
{
    public class GetPagedUsersQueryHandlerTests
    {
        private Mock<ILogger<GetPagedUsersQueryHandler>> mockLogger;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IOrganizationRepository> mockOrganizationRepository;
        private Mock<IMapper> mockMapper;

        private GetPagedUsersQueryHandler underTest;

        public GetPagedUsersQueryHandlerTests()
        {
            mockLogger = new Mock<ILogger<GetPagedUsersQueryHandler>>();
            mockUserRepository = new Mock<IUserRepository>();
            mockOrganizationRepository = new Mock<IOrganizationRepository>();
            mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IList<UserDto>>(_users))
                .Returns(_usersDto);


            underTest = new GetPagedUsersQueryHandler(mockLogger.Object, mockOrganizationRepository.Object, mockUserRepository.Object, mockMapper.Object);
        }

        private const int UserId = 11;

        private static readonly User _user = new User()
        {
            Id = UserId,
            Email = "email@domain.com",
            FirstName = "Alex",
            LastName = "Doe",
            PhoneNumber = "+7788885",
            OrganizationId = OrganizationId
        };

        private static readonly UserDto _userDto = new UserDto()
        {
            Id = UserId,
            Email = "email@domain.com",
            FirstName = "Alex",
            LastName = "Doe",
            PhoneNumber = "+7788885",
            OrganizationId = OrganizationId
        };

        private static readonly User[] _users = new[] {_user, _user};
        private static readonly UserDto[] _usersDto = new[] {_userDto, _userDto};

        private const int OrganizationId = 5;


        [Fact]
        public async Task Handle_Should_GetPagedUsersByOrganizationId()
        {
            // Arrange
            mockOrganizationRepository.Setup(x => x.ExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            const int totalCount = 2;
            mockUserRepository.Setup(x => x.GetPagedUsersByOrganizationId(OrganizationId, It.IsAny<IPagination>()))
                .ReturnsAsync((_users, totalCount));

            var command = new GetPagedUsersQuery() {OrganizationId = OrganizationId};

            // Act
            var result = await underTest.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotEmpty(result.Items);
            Assert.Equal(_usersDto.Length, result.Items.Count);
            Assert.Equal(totalCount, result.PagingInfo.TotalItems);
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_OrganizationNotExist()
        {
            // Arrange
            mockOrganizationRepository.Setup(x => x.ExistAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            var command = new GetPagedUsersQuery() {OrganizationId = OrganizationId};

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => underTest.Handle(command, CancellationToken.None));
        }


    }
}