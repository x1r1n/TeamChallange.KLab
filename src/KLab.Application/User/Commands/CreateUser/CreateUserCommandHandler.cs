using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Emails;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Constants.Emails;
using KLab.Domain.Core.Primitives.ResultModel;
using KLab.Domain.Entities;

namespace KLab.Application.User.Commands.CreateUser
{
	public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<object>>
	{
		private readonly IIdentityService _identityService;
		private readonly IEmailService _emailService;

		public CreateUserCommandHandler(IIdentityService identityService, IEmailService emailService)
		{
			_identityService = identityService;
			_emailService = emailService;
		}

		public async Task<Result<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var uniqueResult = await _identityService.IsEmailUniqueAsync(request.Email);

			if (uniqueResult.isFailure)
			{
				return Result.Failure(uniqueResult.Error);
			}

			var user = new ApplicationUser
			{
				UserName = request.UserName,
				Email = request.Email
			};

			var creationResult = await _identityService.CreateUserAsync(user);

			if (creationResult.isFailure)
			{
				return Result.Failure(creationResult.Error);
			}

			var verificationCode = await _identityService.GenerateEmailVerificationTokenAsync(user);

			await _emailService.SendEmailAsync(
				email: user.Email,
				subject: EmailSubject.Verification,
				body: EmailBody.Verification(user.UserName, verificationCode));

			return Result.Success(new
			{
				user.Email,
				Message = $"The verification code for completing the registration process has been sent to your email address: {user.Email}."
			});
		}
	}
}