using KLab.Domain.Core.Primitives;
using KLab.Domain.Entities;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace KLab.Infrastructure.Core.Extensions
{
	public static class UserManagerExtensions
	{
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