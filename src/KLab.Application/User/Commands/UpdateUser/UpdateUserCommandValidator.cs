using FluentValidation;
using KLab.Application.Core.Errors;

namespace KLab.Application.User.Commands.UpdateUser
{
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(request => request.Id)
				.NotEmpty()
				.WithErrorCode(ValidationErrors.User.UserIdIsRequired);
		}
	}
}
