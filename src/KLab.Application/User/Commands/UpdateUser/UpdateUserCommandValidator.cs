using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.User.Commands.UpdateUser
{
	/// <summary>
	/// Represents a validator for the UpdateUserCommand
	/// </summary>
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		/// <summary>
		/// Validate a user's id
		/// </summary>
		public UpdateUserCommandValidator()
		{
			RuleFor(request => request.Id)
				.NotEmpty()
				.WithError(ValidationErrors.User.IdIsRequired);
		}
	}
}
