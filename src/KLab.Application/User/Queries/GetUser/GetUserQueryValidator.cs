using FluentValidation;
using KLab.Application.Core.Extensions;
using KLab.Domain.Core.Errors;

namespace KLab.Application.User.Queries.GetUser
{
	/// <summary>
	/// Represents a validator for GetUserQuery
	/// </summary>
	public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
	{
		/// <summary>
		/// Vaidate a user id
		/// </summary>
		public GetUserQueryValidator()
		{
			RuleFor(request => request.UserId)
				.NotEmpty()
				.WithError(DomainErrors.Server.InternalError);
		}
	}
}
