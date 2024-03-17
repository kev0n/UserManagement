using System.Net;
using System.Threading.Tasks;
using Common.DTO.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserStorageService.Application.Models;
using UserStorageService.Application.Organizations.Commands;
using UserStorageService.Application.Organizations.Queries;

namespace UserStorageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("attach-user")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> AttachUserToOrganization(AttachUserToOrgCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("users")]
        [ProducesResponseType(typeof(PagedListResult<UserDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPagedUsers(GetPagedUsersQuery query)
        {
            var pagedUsers = await _mediator.Send(query);
            return Ok(pagedUsers);
        }
    }
}