﻿using KLab.Domain.Entities;
using KLab.Infrastructure.Core.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KLab.Infrastructure.Core.Provider
{
	public class CustomEmailTokenProvider : DataProtectorTokenProvider<ApplicationUser>
	{
		public CustomEmailTokenProvider(
			IDataProtectionProvider dataProtectionProvider,
			IOptions<DataProtectionTokenProviderOptions> options,
			ILogger<DataProtectorTokenProvider<ApplicationUser>> logger)
			: base(dataProtectionProvider, options, logger) { }

		public override async Task<string> GenerateAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
		{
			ArgumentNullException.ThrowIfNull(user);
			var ms = new MemoryStream();
			var userId = await manager.GetUserIdAsync(user);
			using (var writer = ms.CreateWriter())
			{
				writer.Write(DateTimeOffset.UtcNow);
				writer.Write(userId);
				writer.Write(purpose ?? "");
				string? stamp = null;
				if (manager.SupportsUserSecurityStamp)
				{
					stamp = await manager.GetSecurityStampAsync(user);
				}
				writer.Write(stamp ?? "");
			}
			var protectedString = Protector.Protect(ms.ToArray()).ToString();
			var hash = protectedString!.GetHashCode();
			var fourDigitNumber = (Math.Abs(hash % 9000) + 1000).ToString();
			return fourDigitNumber;
		}

		public override async Task<bool> ValidateAsync(string purpose, string token, UserManager<ApplicationUser> manager, ApplicationUser user)
		{
			ArgumentNullException.ThrowIfNull(user);
			var ms = new MemoryStream();
			var userId = await manager.GetUserIdAsync(user);
			using (var writer = ms.CreateWriter())
			{
				writer.Write(DateTimeOffset.UtcNow);
				writer.Write(userId);
				writer.Write(purpose ?? "");
				string? stamp = null;
				if (manager.SupportsUserSecurityStamp)
				{
					stamp = await manager.GetSecurityStampAsync(user);
				}
				writer.Write(stamp ?? "");
			}
			var protectedString = Protector.Protect(ms.ToArray()).ToString();
			var hash = protectedString!.GetHashCode();
			var fourDigitNumber = (Math.Abs(hash % 9000) + 1000).ToString();
			return token.Equals(fourDigitNumber);
		}
	}
}