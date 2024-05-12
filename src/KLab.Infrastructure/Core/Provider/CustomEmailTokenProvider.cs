using KLab.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace KLab.Infrastructure.Core.Provider
{
	/// <summary>
	/// Represents a custom email token provider for generating email tokens
	/// </summary>
	public class CustomEmailTokenProvider : DataProtectorTokenProvider<ApplicationUser>
	{
		public CustomEmailTokenProvider(
			IDataProtectionProvider dataProtectionProvider,
			IOptions<DataProtectionTokenProviderOptions> options,
			ILogger<DataProtectorTokenProvider<ApplicationUser>> logger)
			: base(dataProtectionProvider, options, logger) { }

		/// <summary>
		/// Generates a token for the specified purpose and user using Rfc6238TokenService
		/// </summary>
		/// <param name="purpose">The purpose of the token</param>
		/// <param name="manager">The user manager</param>
		/// <param name="user">The user for whom the token is generated</param>
		/// <returns>The generated token</returns>
		public override async Task<string> GenerateAsync(
			string purpose, 
			UserManager<ApplicationUser> manager, 
			ApplicationUser user)
		{
			var token = await manager.CreateSecurityTokenAsync(user);
			var code = Rfc6238TokenService
				.GenerateCode(token, 4)
				.ToString("D4", CultureInfo.InvariantCulture);

			return code;
		}

		/// <summary>
		/// Validates a token for the specified purpose and user
		/// </summary>
		/// <param name="purpose">The purpose of the token</param>
		/// <param name="token">The token to validate</param>
		/// <param name="manager">The user manager</param>
		/// <param name="user">The user for whom the token is validated</param>
		/// <returns>True if the token is valid; otherwise, false</returns>
		public override async Task<bool> ValidateAsync(
			string purpose, string token, 
			UserManager<ApplicationUser> manager, 
			ApplicationUser user)
		{
			if (!Int32.TryParse(token, out int code))
			{
				return false;
			}

			var securityToken = await manager.CreateSecurityTokenAsync(user);
			var valid = Rfc6238TokenService.ValidateCode(securityToken, code, token.Length);

			return valid;
		}
	}
}