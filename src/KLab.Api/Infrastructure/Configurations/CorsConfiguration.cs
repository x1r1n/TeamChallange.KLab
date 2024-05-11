using KLab.Api.Contracts;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace KLab.Api.Infrastructure.Configurations
{
	/// <summary>
	/// Represents a CORS configuration
	/// </summary>
	public static class CorsConfiguration
	{
		/// <summary>
		/// Configures a CORS policy based on the specific origins
		/// </summary>
		/// <param name="builder">The CorsPolicyBuilder used to configure the CORS policy</param>
		/// <param name="configuration">The IConfiguration object containing CORS origins configuration</param>
		/// <returns>The CorsPolicyBuilder instance after configuring CORS policy</returns>
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
