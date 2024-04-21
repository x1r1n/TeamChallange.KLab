using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.Authentication.Commands.ResendVerificationCode
{
	/// <summary>
	/// Represents a validator for ResendVerificationCodeCommand
	/// </summary>
	public class ResendVerificationCodeCommandValidator : AbstractValidator<ResendVerificationCodeCommand>
	{
		/// <summary>
		/// Validate an email
		/// </summary>
		public ResendVerificationCodeCommandValidator()
		{
			RuleFor(request => request.Email)
				.Cascade(CascadeMode.Continue)
				.NotEmpty()
				.WithError(ValidationErrors.Authentication.EmailIsRequired)
				.EmailAddress()
				.WithError(ValidationErrors.Authentication.EmailIsNotValid);
		}
	}
}
