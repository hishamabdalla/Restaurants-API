using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.Register;
using Restaurants.Application.Users.Commands.UpdateUsers;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var isUpdated = await _mediator.Send(command);
            if (isUpdated)
            {
                return NoContent();
            }
            return NotFound();
        }

       

    }
}