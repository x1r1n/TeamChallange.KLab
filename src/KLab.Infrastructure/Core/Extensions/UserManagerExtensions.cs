using KLab.Domain.Core.Primitives;
using KLab.Domain.Entities;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace KLab.Infrastructure.Core.Extensions
{
	/// <summary>
	/// Provides extension methods for UserManager
	/// </summary>
	public static class UserManagerExtensions
	{
		/// <summary>
		/// Finds a user based on the specified criteria
		/// </summary>
		/// <param name="manager">The user manager</param>
		/// <param name="request">The request parameter to search</param>
		/// <param name="type">The type of search criteria</param>
		/// <returns>The ApplicationUser instance if found; otherwise, null</returns>
		public async static Task<ApplicationUser> FindAsync(
			this UserManager<ApplicationUser> manager,
			string request, FindType type) =>
			type switch
			{
				FindType.Id => await manager.FindByIdAsync(request.ToString()),
				FindType.Email => await manager.FindByEmailAsync(request.ToString()),
				FindType.UserName => await manager.FindByNameAsync(request.ToString()),
				_ => null
			};
	}
}