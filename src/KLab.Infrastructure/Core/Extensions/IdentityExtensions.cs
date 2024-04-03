using KLab.Domain.Core.Primitives.ErrorModel;
using Microsoft.AspNetCore.Identity;

namespace KLab.Infrastructure.Core.Extensions
{
	/// <summary>
	/// Provides extension methods for Identity-related functionality
	/// </summary>
	public static class IdentityExtensions
	{
		/// <summary>
		/// Converts a collection of Identity errors to domain errors
		/// </summary>
		/// <param name="errors">The collection of Identity errors to convert</param>
		/// <returns>A collection of domain errors</returns>
		public static IEnumerable<Error> ToDomainErrors(this IEnumerable<IdentityError> error) =>
			error.Select(error => Error.Failure(error.Code, error.Description));
	}
}