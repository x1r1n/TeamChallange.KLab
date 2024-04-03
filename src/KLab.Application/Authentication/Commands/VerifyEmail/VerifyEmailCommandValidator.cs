using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.Authentication.Commands.ConfirmEmail
{
	/// <summary>
	/// Represents a validator for command
	/// </summary>
	public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
	{
		/// <summary>
		/// Validate an email and a verification code
		/// </summary>
		public VerifyEmailCommandValidator()
		{
			RuleFor(request => request.Email)
				.Cascade(CascadeMode.Continue)
				.NotEmpty()
				.WithError(ValidationErrors.Authentication.EmailIsRequired)
				.EmailAddress()
				.WithError(ValidationErrors.Authentication.EmailIsNotValid);

			RuleFor(request => request.VerificationCode)
				.NotEmpty()
				.WithError(ValidationErrors.Authentication.VerificationCodeIsRequired);
		}
	}
}