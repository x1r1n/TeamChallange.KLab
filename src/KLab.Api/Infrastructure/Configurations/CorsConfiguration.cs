using KLab.Api.Contracts;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace KLab.Api.Infrastructure.Configurations
{
	public static class CorsConfiguration
	{
		public static CorsPolicyBuilder ConfigureCorsPolicy(
			this CorsPolicyBuilder builder,
			IConfiguration configuration)
		{
			return builder.WithOrigins(
				configuration[Origins.Local]!,
				configuration[Origins.Network]!,
				configuration[Origins.Production]!)
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials();
		}
	}
}
