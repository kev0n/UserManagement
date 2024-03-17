using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationService.Application.Users.Commands.CreateUserValidation;

namespace UserRegistrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateUser(CreateUserValidationCommand validationCommand)
        {
            await _mediator.Send(validationCommand);
            return Ok();
        }
    }
}