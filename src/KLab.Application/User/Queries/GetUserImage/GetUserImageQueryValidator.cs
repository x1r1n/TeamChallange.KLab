using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.User.Queries.GetUserImage
{
	public class GetUserImageQueryValidator : AbstractValidator<GetUserImageQuery>
	{
        public GetUserImageQueryValidator()
        {
			RuleFor(request => request.Id)
				.NotEmpty()
				.WithError(ValidationErrors.User.IdIsRequired);
		}
    }
}
