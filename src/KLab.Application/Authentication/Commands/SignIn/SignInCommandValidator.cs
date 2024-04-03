using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.Authentication.Commands.SignIn
{
	/// <summary>
	/// Represents a validator for sign in request
	/// </summary>
	public class SignInCommandValidator : AbstractValidator<SignInCommand>
	{
		/// <summary>
		/// Validate an email
		/// </summary>
		public SignInCommandValidator()
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