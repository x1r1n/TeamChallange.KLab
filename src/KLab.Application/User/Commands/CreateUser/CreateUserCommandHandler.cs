using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Emails;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Constants.Emails;
using KLab.Domain.Core.Primitives.ResultModel;
using KLab.Domain.Entities;

namespace KLab.Application.User.Commands.CreateUser
{
	/// <summary>
	/// Represents a command handler for creating a user
	/// </summary>
	public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
	{
		private readonly IIdentityService _identityService;
		private readonly IEmailService _emailService;

		public CreateUserCommandHandler(IIdentityService identityService, IEmailService emailService)
		{
			_identityService = identityService;
			_emailService = emailService;
		}

		/// <summary>
		/// Handles the creation of a user
		/// </summary>
		/// <param name="request">The command to create the user</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var uniqueResult = await _identityService.IsEmailUniqueAsync(request.Email);

			if (uniqueResult.isFailure)
			{
				return Result.Failure(uniqueResult.Errors);
			}

			var user = new ApplicationUser
			{
				UserName = request.UserName,
				Email = request.Email
			};

			var creationResult = await _identityService.CreateUserAsync(user);

			if (creationResult.isFailure)
			{
				return Result.Failure(creationResult.Errors);
			}

			var verificationCode = await _identityService.GenerateEmailVerificationTokenAsync(user);

			await _emailService.SendEmailAsync(
				email: user.Email,
				subject: EmailSubject.Verification,
				body: EmailBody.Verification(user.UserName, verificationCode));

			return Result.Success();
		}
	}
}