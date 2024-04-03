using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;
using KLab.Domain.Core.Constants;

namespace KLab.Application.User.Commands.CreateUser
{
	/// <summary>
	/// Represents a validator for the CreateUserCommand
	/// </summary>
	public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
		/// <summary>
		/// Validate the username and email
		/// </summary>
		public CreateUserCommandValidator()
		{
			RuleFor(request => request.UserName)
				.Cascade(CascadeMode.Continue)
				.NotEmpty()
				.WithError(ValidationErrors.User.UserNameIsRequired)
				.Matches(Pattern.OnlyAlphanumericCharacters)
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