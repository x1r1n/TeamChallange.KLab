using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace KLab.Api.Infrastructure.Configurations
{
	/// <summary>
	/// Represents the swagger configuration
	/// </summary>
	public static class SwaggerConfiguration
	{
		/// <summary>
		/// Configure the SwaggerGen
		/// </summary>
		/// <param name="options">SwaggerGenOptions</param>
		/// <returns>Configured SwaggerGenOptions</returns>
		public static SwaggerGenOptions ConfigureSwaggerGen(this SwaggerGenOptions options)
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "KLab API",
				Version = "v1",
				Description = "The APIs represent the server side of the KLab messenger"
			});

			options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
			{
				Description = "The API key to acces to API",
				Type = SecuritySchemeType.ApiKey,
				In = ParameterLocation.Header,
				Name = "api-key",
				Scheme = "ApiKeyScheme"
			});

			var requirment = new OpenApiSecurityRequirement
			{
				{ 
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "ApiKey"
						},
						In = ParameterLocation.Header
					}, 
					new List<string>() 
				}
			};

			options.AddSecurityRequirement(requirment);

			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			options.IncludeXmlComments(xmlPath);

			return options;
		}
	}
}
