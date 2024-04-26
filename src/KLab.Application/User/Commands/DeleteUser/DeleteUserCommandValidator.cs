using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.User.Commands.DeleteUser
{
	/// <summary>
	/// Represents a validator of DeleteUserCommand
	/// </summary>
	public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
	{
		/// <summary>
		/// Validates a user id
		/// </summary>
		public DeleteUserCommandValidator()
		{
			RuleFor(request => request.Id)
				.NotEmpty()
				.WithError(ValidationErrors.User.IdIsRequired);
		}
	}
}
