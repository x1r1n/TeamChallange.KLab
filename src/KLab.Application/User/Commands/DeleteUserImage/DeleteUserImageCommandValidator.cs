using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.User.Commands.DeleteUserImage
{
	public class DeleteUserImageCommandValidator : AbstractValidator<DeleteUserImageCommand>
	{
		public DeleteUserImageCommandValidator()
		{
			RuleFor(request => request.Id)
				.NotEmpty()
				.WithError(ValidationErrors.User.IdIsRequired);
		}
	}
}
