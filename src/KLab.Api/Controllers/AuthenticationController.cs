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
	/// <summary>
	/// The controller for user registration and authentication
	/// </summary>
	[AllowAnonymous]
	public class AuthenticationController : ApiController
	{
		public AuthenticationController(IMediator mediator)
			: base(mediator)
		{
		}

		/// <summary>
		/// User registration in the application 
		/// </summary>
		/// <remarks>
		/// Note that the username and email must be unique
		/// 
		/// Sample request:
		/// 
		///		POST api/authentication/sign-up
		///		{
		///			"username": "Sofia"
		///			"email": "sofia63@example.com"
		/// }
		/// </remarks>
		/// <param name="request">The scheme of SignUpRequest that represents username and email</param>
		/// <returns>A message informing that the verification code has been sent to the email</returns>
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

		/// <summary>
		/// User authentication in the application
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// 
		///		POST api/authentication/sign-in
		///		{
		///			"email": "sofia63@example.com"
		///		}
		/// </remarks>
		/// <param name="request">The scheme of SignInRequest that represents email</param>
		/// <returns>A message informing that the authentication code has been sent to the email</returns>
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

		/// <summary>
		/// Ending a user session and sign out of the application
		/// </summary>
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

		/// <summary>
		/// Confirmation of an email with the specified confirmation code for new user registration
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// 
		///		POST api/authentication/verify-email
		///		{
		///			"email": "sofia63@example.com"
		///			"verificationCode": "4813"
		///		}
		/// </remarks>
		/// <param name="request">The scheme of VerifyEmailRequest that represents email and verification code</param>
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

		/// <summary>
		/// Authenticate a user using the authentication code sent via email
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// 
		///		POST api/authentication/authenticate-email
		///		{
		///			"email": "sofia63@example.com"
		///			"authenticationCode": "4813"
		///		}
		/// </remarks>
		/// <param name="request">The scheme of AuthenticateRequest that represents email and authentication code</param>
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