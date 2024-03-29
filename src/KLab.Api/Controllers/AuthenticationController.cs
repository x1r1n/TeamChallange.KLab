﻿using KLab.Api.Contracts;
using KLab.Api.Infrastructure;
using KLab.Application.Authentication.Commands.Authenticate;
using KLab.Application.Authentication.Commands.ConfirmEmail;
using KLab.Application.Authentication.Commands.SignIn;
using KLab.Application.User.Commands.CreateUser;
using KLab.Contracts.Authentication;
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
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> SignUp(SignUpRequest request)
		{
			var result = await _mediator.Send(new CreateUserCommand(request.UserName, request.Email));

			if (result.isFailure)
			{
				return BadRequest(result.Error);
			}

			return Ok(DomainResponses.Email.VerificationCodeSent);
		}

		[HttpPost(ApiRoutes.Authentication.SignIn)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> SignIn(SignInRequest request)
		{
			var result = await _mediator.Send(new SignInCommand(request.Email));

			if (result.isFailure)
			{
				return BadRequest(result.Error);
			}

			return Ok(DomainResponses.Email.AuthenticationCodeSent);
		}

		[HttpPost(ApiRoutes.Authentication.VerifyEmail)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
		{
			var result = await _mediator.Send(new VerifyEmailCommand(request.Email, request.VerificationCode));

			if (result.isFailure)
			{
				return BadRequest(result.Error);
			}

			return Ok();
		}

		[HttpPost(ApiRoutes.Authentication.Authenticate)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Authenticate(AuthenticateRequest request)
		{
			var result = await _mediator.Send(new AuthenticateCommand(request.Email, request.AuthenticationCode));

			if (result.isFailure)
			{
				return BadRequest(result.Error);
			}

			return Ok();
		}
	}
}