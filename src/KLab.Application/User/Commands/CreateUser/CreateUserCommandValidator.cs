using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;
using KLab.Domain.Core.Constants.Regex;

namespace KLab.Application.User.Commands.CreateUser
{
	public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserCommandValidator()
		{
			RuleFor(request => request.UserName)
				.Cascade(CascadeMode.Continue)
				.NotEmpty()
				.WithError(ValidationErrors.User.UserNameIsRequired)
				.Matches(Pattern.OnlyAlphanumericCharactersWithUnderScore)
				.WithError(ValidationErrors.User.UserNameIsNotValid);

			RuleFor(request => request.Email)
				.Cascade(CascadeMode.Continue)
				.NotEmpty()
				.WithError(ValidationErrors.User.EmailIsRequired)
				.EmailAddress()
				.WithError(ValidationErrors.User.EmailIsNotValid);
		}
	}
}