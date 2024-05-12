using KLab.Api.Contracts;
using KLab.Api.Infrastructure;
using KLab.Application.Authentication.Commands.Authenticate;
using KLab.Application.Authentication.Commands.ConfirmEmail;
using KLab.Application.Authentication.Commands.ResendVerificationCode;
using KLab.Application.Authentication.Commands.SignIn;
using KLab.Application.Authentication.Commands.SignOut;
using KLab.Application.User.Commands.CreateUser;
using KLab.Contracts.Authentication;
using KLab.Domain.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLab.Api.Controllers
{
	/// <summary>
	/// The controller for user registration and authentication
	/// </summary>
	public class AuthenticationController : ApiController
	{
		public AuthenticationController(IMediator mediator)
			: base(mediator)
		{
		}

		/// <summary>
		/// Registers a new user with the provided sign up information
		/// </summary>
		/// <remarks>
		/// <param name="request">The sign up request containing user details</param>
		/// <returns>A message informing that the verification code has been sent to the email</returns>
		/// <response code="200">If verification code to complete registration is sent </response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="409">If a username or email already exists</response>
		/// <response code="422">If the sign up process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPost(ApiRoutes.Authentication.SignUp)]
		[AllowAnonymous]
		public async Task<IActionResult> SignUp(SignUpRequest request)
		{
			var result = await _mediator.Send(new CreateUserCommand(request.Username, request.Email));

			return result.IsSuccess
				? Ok(DomainResponses.Email.VerificationCodeSent)
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Authenticates a user using email
		/// </summary>
		/// <param name="request">The sign in request containing user email</param>
		/// <response code="200">If the authentication code to complete authentication is sent</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="404">If the requested resource is not found</response>
		/// <response code="422">If the sign in process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPost(ApiRoutes.Authentication.SignIn)]
		[AllowAnonymous]
		public async Task<IActionResult> SignIn(SignInRequest request)
		{
			var result = await _mediator.Send(new SignInCommand(request.Email));

			return result.IsSuccess
				? Ok(DomainResponses.Email.AuthenticationCodeSent)
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Signs out the currently authenticated user
		/// </summary>
		/// <response code="204">If the user is successfully signed out</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPost(ApiRoutes.Authentication.SignOut)]
		public async Task<IActionResult> UserSignOut()
		{
			var result = await _mediator.Send(new SignOutCommand());

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Verifies the email address using the provided verification code
		/// </summary>
		/// <param name="request">The request containing email address and verification code</param>
		/// <response code="204">If the email address is successfully verified</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="404">If the user is not found</response>
		/// <response code="422">If the verification process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPost(ApiRoutes.Authentication.VerifyEmail)]
		[AllowAnonymous]
		public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
		{
			var result = await _mediator.Send(new VerifyEmailCommand(request.Email, request.VerificationCode));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Authenticates a user with the provided email and authentication code
		/// </summary>
		/// <param name="request">The request containing email address and authentication code</param>
		/// <response code="204">If the user is successfully authenticated</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="404">If the user is not found</response>
		/// <response code="422">If the authentication process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPost(ApiRoutes.Authentication.Authenticate)]
		[AllowAnonymous]
		public async Task<IActionResult> Authenticate(AuthenticateRequest request)
		{
			var result = await _mediator.Send(new AuthenticateCommand(request.Email, request.AuthenticationCode));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}

		/// <summary>
		/// Resends the verification code to the specified email address
		/// </summary>
		/// <param name="request">The request containing the email address</param>
		/// <response code="200">If the verification code is successfully resent</response>
		/// <response code="400">If the request is invalid or malformed</response>
		/// <response code="404">If the user is not found</response>
		/// <response code="422">If the resend verification code process encounters validation errors</response>
		/// <response code="500">If an unexpected error occurs during processing</response>
		[HttpPost(ApiRoutes.Authentication.ResendVerificationCode)]
		[AllowAnonymous]
		public async Task<IActionResult> ResendVerificationCode(ResendVerificationCodeRequest request)
		{
			var result = await _mediator.Send(new ResendVerificationCodeCommand(request.Email!));

			return result.IsSuccess
				? Ok(DomainResponses.Email.VerificationCodeSent)
				: HandleFailure(result.Errors);
		}
	}
}