using KLab.Api.Contracts;
using KLab.Api.Infrastructure;
using KLab.Api.Infrastructure.Filters;
using KLab.Application.User.Commands.DeleteUserImage;
using KLab.Application.User.Commands.UpdateUser;
using KLab.Application.User.Commands.UpdateUserImage;
using KLab.Application.User.Commands.UploadUserImage;
using KLab.Application.User.Queries.GetUser;
using KLab.Application.User.Queries.GetUserImage;
using KLab.Contracts.User;
using KLab.Domain.Core.Primitives.ErrorModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KLab.Api.Controllers
{
	/// <summary>
	/// The controller for user management
	/// </summary>
	public class UsersController : ApiController
	{
		public UsersController(IMediator sender)
			: base(sender)
		{
		}

		/// <summary>
		/// Get information about current user
		/// </summary>
		/// <returns>The information about current user: id, username, nickname, email and registration date and time</returns>
		[HttpGet(ApiRoutes.Users.Me)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetMe()
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var result = await _mediator.Send(new GetUserQuery(userId!));

			return result.IsSuccess
				? Ok(result.Value)
				: NotFound(result.Errors);
		}

		/// <summary>
		/// Get information about user with specified id
		/// </summary>
		/// <returns>The information about user: id, username, nickname, email and registration date and time</returns>
		[HttpGet(ApiRoutes.Users.User)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetUser(string id)
		{
			var result = await _mediator.Send(new GetUserQuery(id));

			return result.IsSuccess
				? Ok(result.Value)
				: NotFound(result.Errors);
		}

		/// <summary>
		/// Partial or full update of current user information
		/// </summary>
		/// <remarks>
		/// Sample requests:
		/// 
		///     PATCH api/users/{id}
		///     {
		///         "nickname": "kryakazyabra",
		///         "description": "I like foreign and domestic literature"
		///     }
		///     
		///     PATCH api/users/{id}
		///     {
		///         "description": "I hope to find like-minded people with whom I can discuss ukrainian literature"
		///     }
		/// </remarks>
		/// <param name="id">The user id</param>
		/// <param name="request">The scheme of UpdateUserRequest that represents updatable data</param>
		[HttpPatch(ApiRoutes.Users.User)]
		[UserIdComparisonFilter]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
		{
			var result = await _mediator.Send(new UpdateUserCommand(
				id,
				request.Nickname!,
				request.Description!));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Get an image of user with specified id
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>The file with a content type that represents an image</returns>
		[HttpGet(ApiRoutes.Users.Image)]
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

		/// <summary>
		/// Set an image for current user
		/// </summary>
		/// <param name="id">The user id</param>
		/// <param name="request">The sheme of UploadUserImageRequest that represents image</param>
		[HttpPost(ApiRoutes.Users.Image)]
		[UserIdComparisonFilter]
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

		/// <summary>
		/// Update an image of current user 
		/// </summary>
		/// <param name="id">The user id</param>
		/// <param name="request">The scheme of UpdateUserImageRequest that represents image</param>
		[HttpPut(ApiRoutes.Users.Image)]
		[UserIdComparisonFilter]
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

		/// <summary>
		/// Delete an image of current user, after which the user will be without an image
		/// </summary>
		/// <param name="id">The user id</param>
		[HttpDelete(ApiRoutes.Users.Image)]
		[UserIdComparisonFilter]
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