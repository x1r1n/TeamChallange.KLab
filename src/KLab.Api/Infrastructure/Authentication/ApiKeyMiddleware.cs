using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ErrorModel;
using System.Net;

namespace KLab.Api.Infrastructure.Authentication
{
	public class ApiKeyMiddleware : IMiddleware
	{
		private readonly IConfiguration _configuration;

		public ApiKeyMiddleware(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (!context.Request.Headers.TryGetValue(ApiKeyConstants.Header, out var extractedApiKey))
			{
				await HandleResponseAsync(context, DomainErrors.ClientAuthentication.MissingApiKey);

				return;
			}

			var apiKey = _configuration[ApiKeyConstants.Section];

			if (string.IsNullOrWhiteSpace(apiKey))
			{
				await HandleResponseAsync(context, DomainErrors.ClientAuthentication.UninitializedApiKey);

				return;
			}

			if (!apiKey.Equals(extractedApiKey))
			{
				await HandleResponseAsync(context, DomainErrors.ClientAuthentication.IncorrectApiKey);

				return;
			}

			await next(context);
		}

		private Task HandleResponseAsync(HttpContext context, Error error)
		{
			context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			context.Response.ContentType = "application/json";

			return context.Response.WriteAsJsonAsync(error);
		}
	}
}