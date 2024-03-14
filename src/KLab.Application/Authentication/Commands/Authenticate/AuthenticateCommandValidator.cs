using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.Authentication.Commands.Authenticate
{
	public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
	{
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