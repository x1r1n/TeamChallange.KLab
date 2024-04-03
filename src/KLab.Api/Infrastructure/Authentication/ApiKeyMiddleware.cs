using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ErrorModel;
using System.Net;

namespace KLab.Api.Infrastructure.Authentication
{
	/// <summary>
	/// The middleware for client authorization using the api key
	/// </summary>
	public class ApiKeyMiddleware : IMiddleware
	{
		private readonly IConfiguration _configuration;

		public ApiKeyMiddleware(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Handle incoming api key
		/// </summary>
		/// <param name="context">The HTTP context</param>
		/// <param name="next">The delegate to invoke the next handler in the pipeline</param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (!context.Request.Headers.TryGetValue(ApiKeyConstants.Header, out var extractedApiKey))
			{
				await ConfigureResponseAsync(context, DomainErrors.ClientAuthentication.MissingApiKey);

				return;
			}

			var apiKey = _configuration[ApiKeyConstants.Section];

			if (string.IsNullOrWhiteSpace(apiKey))
			{
				await ConfigureResponseAsync(context, DomainErrors.ClientAuthentication.UninitializedApiKey);

				return;
			}

			if (!apiKey.Equals(extractedApiKey))
			{
				await ConfigureResponseAsync(context, DomainErrors.ClientAuthentication.IncorrectApiKey);

				return;
			}

			await next(context);
		}

		/// <summary>
		/// Configure response for incorrect api key
		/// </summary>
		/// <param name="context">The HTTP context</param>
		/// <param name="error">The error with code and message</param>
		private async Task ConfigureResponseAsync(HttpContext context, Error error)
		{
			context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			context.Response.ContentType = "application/json";

			await context.Response.WriteAsJsonAsync(error);
		}
	}
}