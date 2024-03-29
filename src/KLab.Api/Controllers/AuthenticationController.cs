using KLab.Api.Contracts;
using KLab.Api.Infrastructure;
using KLab.Application.Authentication.Commands.Authenticate;
using KLab.Application.Authentication.Commands.ConfirmEmail;
using KLab.Application.Authentication.Commands.SignIn;
using KLab.Application.Authentication.Commands.SignOut;
using KLab.Application.User.Commands.CreateUser;
using KLab.Contracts.Authentication;
using KLab.Domain.Core.Primitives.ErrorModel;
using KLab.Domain.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLab.Api.Controllers
{
    [AllowAnonymous]
	public class AuthenticationController : ApiController
	{
		public AuthenticationController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpPost(ApiRoutes.Authentication.SignUp)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> SignUp(SignUpRequest request)
		{
			var result = await _mediator.Send(new CreateUserCommand(request.Username, request.Email));

			return result.IsSuccess
				? Ok(DomainResponses.Email.VerificationCodeSent)
				: HandleFailure(result.Errors);
		}

		[HttpPost(ApiRoutes.Authentication.SignIn)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> SignIn(SignInRequest request)
		{
			var result = await _mediator.Send(new SignInCommand(request.Email));

			return result.IsSuccess
				? Ok(DomainResponses.Email.AuthenticationCodeSent)
				: HandleFailure(result.Errors);
		}

		[HttpPost(ApiRoutes.Authentication.SignOut)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UserSignOut()
		{
			var result = await _mediator.Send(new SignOutCommand());

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		[HttpPost(ApiRoutes.Authentication.VerifyEmail)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
		{
			var result = await _mediator.Send(new VerifyEmailCommand(request.Email, request.VerificationCode));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		[HttpPost(ApiRoutes.Authentication.Authenticate)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status422UnprocessableEntity)]
		public async Task<IActionResult> Authenticate(AuthenticateRequest request)
		{
			var result = await _mediator.Send(new AuthenticateCommand(request.Email, request.AuthenticationCode));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}
	}
}