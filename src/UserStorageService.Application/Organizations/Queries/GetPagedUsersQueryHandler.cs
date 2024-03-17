using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO.Users;
using Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using UserStorageService.Application.Models;

namespace UserStorageService.Application.Organizations.Queries
{
    public class GetPagedUsersQueryHandler : IRequestHandler<GetPagedUsersQuery, PagedListResult<UserDto>>
    {
        private readonly ILogger<GetPagedUsersQueryHandler> _logger;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetPagedUsersQueryHandler(
            ILogger<GetPagedUsersQueryHandler> logger,
            IOrganizationRepository organizationRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _logger = logger;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _mapper = mapper;

        }

        public async Task<PagedListResult<UserDto>> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var organizationExist = await _organizationRepository.ExistAsync(request.OrganizationId);

            if (!organizationExist)
            {
                _logger.LogError("Organization with id={OrganizationId} not found", request.OrganizationId);
                throw new NotFoundException($"Organization with id={request.OrganizationId} not found");
            }

            if (request.PageSize <= 0) request.PageSize = 10;
            if (request.Page <= 0) request.Page = 1;

            var result = await _userRepository.GetPagedUsersByOrganizationId(request.OrganizationId, request);
            var pagedResult = new PagedListResult<UserDto>()
            {
                Items = _mapper.Map<IList<UserDto>>(result.users),
                PagingInfo = new PagingInfo()
                {
                    Page = request.Page,
                    PageSize = request.PageSize,
                    TotalItems = result.totalCount
                }
            };

            return pagedResult;
        }
    }
}