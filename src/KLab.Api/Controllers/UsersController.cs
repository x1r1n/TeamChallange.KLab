using KLab.Api.Contracts;
using KLab.Api.Infrastructure;
using KLab.Application.User.Commands.DeleteUserImage;
using KLab.Application.User.Commands.UpdateUser;
using KLab.Application.User.Commands.UpdateUserImage;
using KLab.Application.User.Commands.UploadUserImage;
using KLab.Application.User.Queries.GetUser;
using KLab.Application.User.Queries.GetUserImage;
using KLab.Contracts.User;
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
		public UsersController(IMediator mediator)
			: base(mediator)
		{
		}

		/// <summary>
		/// Retrieves information about the currently authenticated user
		/// </summary>
		/// <remarks>
		/// Returns the user id, username, nickname, email, description, role and registration date with time in UTC
		/// </remarks>
		/// <response code="200">If the user information is successfully retrieved</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="401">If the user is unauthorized due to lack or invalid credentials</response>
		/// <response code="404">If the user is not found</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpGet(ApiRoutes.Users.Me)]
		public async Task<IActionResult> GetMe()
		{
			var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var result = await _mediator.Send(new GetUserQuery(id!));

			return result.IsSuccess
				? Ok(result.Value)
				: NotFound(result.Errors);
		}

		/// <summary>
		/// Retrieves information about a user by their id
		/// </summary>
		/// <remarks>
		/// Returns the user id, username, nickname, email, description, role and registration date with time in UTC
		/// </remarks>
		/// <param name="id">The id of the user to retrieve information for.</param>
		/// <response code="200">If the user information is successfully retrieved</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="404">If the user is not found</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpGet(ApiRoutes.Users.User)]
		public async Task<IActionResult> GetUser(string id)
		{
			var result = await _mediator.Send(new GetUserQuery(id));

			return result.IsSuccess
				? Ok(result.Value)
				: NotFound(result.Errors);
		}

		/// <summary>
		/// Updates information about the currently authenticated user
		/// </summary>
		/// <param name="request">The request containing updated user information</param>
		/// <response code="204">If the user information is successfully updated</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="401">If the user is unauthorized due to lack or invalid credentials</response>
		/// <response code="404">If the user is not found</response>
		/// <response code="422">If the update process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPatch(ApiRoutes.Users.Me)]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
		{
			var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var result = await _mediator.Send(new UpdateUserCommand(
				id!,
				request.Nickname!,
				request.Description!));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Retrieves the image of a user by their id
		/// </summary>
		/// <remarks>
		/// The response contains the image data in the form of a file stream, 
		/// along with information about the content type and file name
		/// </remarks>
		/// <param name="id">The id of the user to retrieve the image for</param>
		/// <response code="200">If the user image is successfully retrieved</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="404">If the user or image is not found</response>
		/// <response code="422">If the image retrieval process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpGet(ApiRoutes.Users.UserImage)]
		public async Task<IActionResult> GetUserImage(string id)
		{
			var result = await _mediator.Send(new GetUserImageQuery(id));

			return result.IsSuccess
				? File(result.Value.Content, result.Value.ContentType, result.Value.Name)
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Uploads an image for the currently authenticated user
		/// </summary>
		/// <param name="request">The request containing the image data</param>
		/// <response code="204">If the user image is successfully uploaded</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="401">If the user is unauthorized due to lack or invalid credentials</response>
		/// <response code="404">If the user is not found</response>
		/// <response code="409">If the user already has an image uploaded</response>
		/// <response code="422">If the image upload process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPost(ApiRoutes.Users.MeImage)]
		public async Task<IActionResult> UploadUserImage(UploadUserImageRequest request)
		{
			var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var result = await _mediator.Send(new UploadUserImageCommand(
				id!,
				request.Image!));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Updates the image for the currently authenticated user
		/// </summary>
		/// <param name="request">The request containing the updated image data</param>
		/// <response code="204">If the user image is successfully updated</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="401">If the user is unauthorized due to lack or invalid credentials</response>
		/// <response code="404">If the user is not found</response>
		/// <response code="422">If the image upload process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPut(ApiRoutes.Users.MeImage)]
		public async Task<IActionResult> UpdateUserImage(UpdateUserImageRequest request)
		{
			var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var result = await _mediator.Send(new UpdateUserImageCommand(
				id!,
				request.Image!));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Deletes the image of the currently authenticated user
		/// </summary>
		/// <response code="204">If the user image is successfully deleted</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="401">If the user is unauthorized due to lack or invalid credentials</response>
		/// <response code="404">If the user or image is not found</response>
		/// <response code="422">If the image deletion process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpDelete(ApiRoutes.Users.MeImage)]
		public async Task<IActionResult> DeleteUserImage()
		{
			var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var result = await _mediator.Send(new DeleteUserImageCommand(id!));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}
	}
}