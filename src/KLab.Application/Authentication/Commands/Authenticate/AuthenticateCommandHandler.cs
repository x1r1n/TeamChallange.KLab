﻿using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.Authenticate
{
	/// <summary>
	/// Represents a handler for authenticate user
	/// </summary>
	public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public AuthenticateCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		/// <summary>
		/// Handle the authentication command for authenticate user by validating the user's email and authentication code
		/// and authenticates the user
		/// </summary>
		/// <param name="request">AuthenticateCommand that represent email and authentication code</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of user authentication</returns>
		public async Task<Result> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
		{
			var foundResult = await _identityService.FindUserAsync(request.Email!, FindType.Email);

			if (foundResult.isFailure)
			{
				return Result.Failure(foundResult.Errors);
			}

			var user = foundResult.Value;

			if (!user.EmailConfirmed)
			{
				return Result.Failure(DomainErrors.Authentication.UnverifiedEmail);
			}

			var authenticateResult = await _identityService.AuthenticateUserAsync(user, request.AuthenticationCode!);

			return authenticateResult.IsSuccess 
				? Result.Success()
				: Result.Failure(authenticateResult.Errors);
		}
	}
}