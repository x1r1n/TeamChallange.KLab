using KLab.Api.Contracts;
using KLab.Api.Infrastructure;
using KLab.Application.User.Commands.DeleteUserImage;
using KLab.Application.User.Commands.UpdateUser;
using KLab.Application.User.Commands.UpdateUserImage;
using KLab.Application.User.Commands.UploadUserImage;
using KLab.Application.User.Queries.GetUser;
using KLab.Application.User.Queries.GetUserImage;
using KLab.Contracts.User;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ErrorModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

			return result.IsSuccess
				? Ok(result.Value)
				: NotFound(result.Errors);
		}

		[HttpPatch(ApiRoutes.Users.Update)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
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

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		[HttpGet(ApiRoutes.Users.GetImage)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> GetUserImage(string id)
		{
			var result = await _mediator.Send(new GetUserImageQuery(id));

			return result.IsSuccess
				? File(result.Value.Content, result.Value.ContentType, result.Value.Name)
				: HandleFailure(result.Errors);
		}

		[HttpPost(ApiRoutes.Users.UploadImage)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> UploadUserImage(string id, UploadUserImageRequest request)
		{
			var result = await _mediator.Send(new UploadUserImageCommand(
				id,
				request.Image!));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		[HttpPut(ApiRoutes.Users.UpdateImage)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> UpdateUserImage(string id, UpdateUserImageRequest request)
		{
			var result = await _mediator.Send(new UpdateUserImageCommand(
				id,
				request.Image!));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		[HttpDelete(ApiRoutes.Users.DeleteImage)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> DeleteUserImage(string id)
		{
			var result = await _mediator.Send(new DeleteUserImageCommand(id));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}
	}
}