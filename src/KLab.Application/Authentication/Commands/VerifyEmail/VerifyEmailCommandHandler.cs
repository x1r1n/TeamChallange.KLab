using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.ConfirmEmail
{
	/// <summary>
	/// Represents handler for verify email and complete registration
	/// </summary>
	public class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public VerifyEmailCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		/// <summary>
		/// Verify email using verification code and complete registration
		/// </summary>
		/// <param name="request">VerifyEmailCommand that represent email and verification code</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of email verification</returns>
		public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
		{
			var foundResult = await _identityService.FindUserAsync(request.Email!, FindType.Email);

			if (foundResult.isFailure)
			{
				return Result.Failure(foundResult.Errors);
			}

			var user = foundResult.Value;

			var verifiedResult = await _identityService.VerifyEmailAsync(user, request.VerificationCode!);

			return verifiedResult.IsSuccess 
				? Result.Success() 
				: Result.Failure(verifiedResult.Errors);
		}
	}
}