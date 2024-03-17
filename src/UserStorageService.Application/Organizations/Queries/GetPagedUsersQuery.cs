using Common.DTO.Users;
using Domain.Interfaces;
using MediatR;
using UserStorageService.Application.Models;

namespace UserStorageService.Application.Organizations.Queries
{
    public class GetPagedUsersQuery : IRequest<PagedListResult<UserDto>>, IPagination
    {
        public int OrganizationId { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}