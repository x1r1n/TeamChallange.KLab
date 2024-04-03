using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.Authentication.Commands.Authenticate
{
	/// <summary>
	/// Represents a validator for authentication request
	/// </summary>
	public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
	{
		/// <summary>
		/// Validate email and authentication code
		/// </summary>
		public AuthenticateCommandValidator()
		{
			RuleFor(request => request.Email)
				.Cascade(CascadeMode.Continue)
				.NotEmpty()
				.WithError(ValidationErrors.Authentication.EmailIsRequired)
				.EmailAddress()
				.WithError(ValidationErrors.Authentication.EmailIsNotValid);

			RuleFor(request => request.AuthenticationCode)
				.NotEmpty()
				.WithError(ValidationErrors.Authentication.AuthenticationCodeIsRequired);
		}
	}
}