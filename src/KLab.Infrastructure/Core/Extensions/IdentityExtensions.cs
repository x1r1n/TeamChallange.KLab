using KLab.Application.User.Commands.UpdateUser;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ErrorModel;
using KLab.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KLab.Infrastructure.Core.Extensions
{
	public static class IdentityExtensions
	{
		public static IEnumerable<Error> ToDomainErrors(this IEnumerable<IdentityError> error) =>
			error.Select(error => Error.Failure(error.Code, error.Description));
	}
}