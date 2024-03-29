using FluentValidation;
using KLab.Application.Core.Extensions;
using KLab.Domain.Core.Errors;

namespace KLab.Application.User.Queries.GetUser
{
	public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
	{
		public GetUserQueryValidator()
		{
			RuleFor(request => request.UserName)
				.NotEmpty()
				.WithError(DomainErrors.Server.InternalError);
		}
	}
}
