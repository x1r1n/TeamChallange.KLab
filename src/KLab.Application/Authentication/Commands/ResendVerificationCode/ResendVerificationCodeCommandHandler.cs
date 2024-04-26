using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Emails;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Constants.Emails;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.ResendVerificationCode
{
	/// <summary>
	/// Represents a handler to resend a verification code
	/// </summary>
	public class ResendVerificationCodeCommandHandler : ICommandHandler<ResendVerificationCodeCommand, Result>
	{
		private readonly IIdentityService _identityService;
		private readonly IEmailService _emailService;

		public ResendVerificationCodeCommandHandler(IIdentityService identityService, IEmailService emailService)
		{
			_identityService = identityService;
			_emailService = emailService;
		}
		
		/// <summary>
		/// Handles the command to resend a verification code via email.
		/// </summary>
		/// <param name="request">The command containing the email address of the user.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The result indicating success or failure.</returns>
		public async Task<Result> Handle(ResendVerificationCodeCommand request, CancellationToken cancellationToken)
		{
			var findUserResult = await _identityService.FindUserAsync(request.Email, FindType.Email);

			if (findUserResult.isFailure)
			{
				return Result.Failure(findUserResult.Errors);
			}

			var user = findUserResult.Value;

			if (user.EmailConfirmed)
			{
				return Result.Failure(DomainErrors.Authentication.EmailAlreadyVerified);
			}

			var verificationCode = await _identityService.GenerateEmailVerificationTokenAsync(user);

			await _emailService.SendEmailAsync(
				email: user.Email!,
				subject: EmailSubject.Verification,
				body: EmailBody.Verification(user.UserName!, verificationCode));

			return Result.Success();
		}
	}
}
