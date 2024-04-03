using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.User.Commands.DeleteUserImage
{
	/// <summary>
	/// Represents a validator for the DeleteUserImageCommand
	/// </summary>
	public class DeleteUserImageCommandValidator : AbstractValidator<DeleteUserImageCommand>
	{
		/// <summary>
		/// Validate a user's id
		/// </summary>
		public DeleteUserImageCommandValidator()
		{
			RuleFor(request => request.Id)
				.NotEmpty()
				.WithError(ValidationErrors.User.IdIsRequired);
		}
	}
}
