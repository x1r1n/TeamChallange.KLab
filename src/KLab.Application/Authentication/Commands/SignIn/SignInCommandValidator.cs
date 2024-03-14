using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.Authentication.Commands.SignIn
{
	public class SignInCommandValidator : AbstractValidator<SignInCommand>
	{
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