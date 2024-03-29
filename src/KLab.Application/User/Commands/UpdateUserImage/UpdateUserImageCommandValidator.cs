using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.User.Commands.UpdateUserImage
{
	public class UpdateUserImageCommandValidator : AbstractValidator<UpdateUserImageCommand>
	{
		public UpdateUserImageCommandValidator()
		{
			RuleFor(request => request.Id)
				.NotEmpty()
				.WithError(ValidationErrors.User.IdIsRequired);

			RuleFor(request => request.Image.Length)
				.NotEmpty()
				.WithError(ValidationErrors.User.ImageIsRequired);

			RuleFor(request => request.Image.ContentType)
				.Cascade(CascadeMode.Continue)
				.NotEmpty()
				.WithError(ValidationErrors.User.ImageContentTypeIsRequired)
				.Must(type => type.Equals("image/jpeg") || type.Equals("image/jpg") || type.Equals("image/png"))
				.WithError(ValidationErrors.User.FileMustBeImage);
		}
	}
}
