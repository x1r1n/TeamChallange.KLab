using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KLab.Api.Infrastructure.Configurations
{
	public static class SwaggerExtensions
	{
		public static SwaggerGenOptions AddApiKeySecurity(this SwaggerGenOptions options)
		{
			options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
			{
				Description = "The API key to acces to API",
				Type = SecuritySchemeType.ApiKey,
				Name = "api-key",
				In = ParameterLocation.Header,
				Scheme = "ApiKeyScheme"
			});

			var scheme = new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "ApiKey"
				},
				In = ParameterLocation.Header
			};

			var requirment = new OpenApiSecurityRequirement
			{
				{ scheme, new List<string>() }
			};

			options.AddSecurityRequirement(requirment);

			return options;
		}
	}
}