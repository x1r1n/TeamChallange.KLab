using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Emails;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Constants.Emails;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.SignIn
{
	/// <summary>
	/// Represents a handler for user sign in
	/// </summary>
	public class SignInCommandHandler : ICommandHandler<SignInCommand, Result>
	{
		private readonly IIdentityService _identityService;
		private readonly IEmailService _emailService;

		public SignInCommandHandler(IIdentityService identityService, IEmailService emailService)
		{
			_identityService = identityService;
			_emailService = emailService;
		}

		/// <summary>
		/// Validate request, generate authentication token and send to email for authenticate user
		/// </summary>
		/// <param name="request">SignInCommand that represent email</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of user sign in</returns>
		public async Task<Result> Handle(SignInCommand request, CancellationToken cancellationToken)
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

			var authenticationCode = await _identityService.GenerateAuthenticationTokenAsync(user);

			await _emailService.SendEmailAsync(
				email: user.Email!,
				subject: EmailSubject.Authentication,
				body: EmailBody.Authentication(user.UserName!, authenticationCode));

			return Result.Success();
		}
	}
}