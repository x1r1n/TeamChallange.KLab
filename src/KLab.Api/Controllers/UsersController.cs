using KLab.Api.Contracts;
using KLab.Api.Infrastructure;
using KLab.Application.User.Commands.UpdateUser;
using KLab.Application.User.Queries.GetUser;
using KLab.Contracts.User;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ErrorModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KLab.Api.Controllers
{
	public class UsersController : ApiController
	{
        public UsersController(IMediator sender)
            : base(sender) 
        { 
        }

		[HttpGet(ApiRoutes.Users.Me)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetMe()
		{
			if (HttpContext.User.Identity is null)
			{
				return Unauthorized(DomainErrors.Authentication.Unauthorized);
			}

			var username = HttpContext.User.Identity.Name;

			var result = await _mediator.Send(new GetUserQuery(username!));

			if (result.isFailure)
			{
				return NotFound(result.Errors);
			}

			return Ok(result.Value);
		}

		[HttpPatch(ApiRoutes.Users.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
			if (request is null)
            {
                return BadRequest(Error.Failure(
                    "RequestIsRequired",
                    "The request is required."));
            }

            var result = await _mediator.Send(new UpdateUserCommand(
                id, 
                request.Nickname!, 
                request.Description!));

            if (result.isFailure)
            {
                return HandleBadRequest(result.Errors);
            }

            return Ok();
        }
	}
}
