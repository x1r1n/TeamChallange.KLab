using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.User.Queries.GetUserImage
{
	/// <summary>
	/// Represents a validator for GetUserImageQuery
	/// </summary>
	public class GetUserImageQueryValidator : AbstractValidator<GetUserImageQuery>
	{
		/// <summary>
		/// Validate a user's id
		/// </summary>
        public GetUserImageQueryValidator()
        {
			RuleFor(request => request.Id)
				.NotEmpty()
				.WithError(ValidationErrors.User.IdIsRequired);
		}
    }
}
