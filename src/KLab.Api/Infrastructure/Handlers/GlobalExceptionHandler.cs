using FluentValidation;
using KLab.Domain.Core.Primitives.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;

namespace KLab.Api.Infrastructure.Handlers
{
	public class GlobalExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			var statusCode = GetStatusCode(exception);

			var response = new
			{
				Status = statusCode,
				Detail = GetDetail(exception),
				Errors = GetErrors(exception)
			};

			httpContext.Response.ContentType = "application/json";

			httpContext.Response.StatusCode = statusCode;

			await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

			return true;
		}

		private static int GetStatusCode(Exception exception) =>
			exception switch
			{
				ValidationException => StatusCodes.Status422UnprocessableEntity,
				_ => StatusCodes.Status500InternalServerError
			};

		private static string GetDetail(Exception exception) =>
			exception is ValidationException ? "Validation failed" : exception.Message;

		private static IEnumerable<Error> GetErrors(Exception exception) =>
			exception is ValidationException validationException
			? validationException.Errors.Select(error => Error.Validition(error.ErrorCode, error.ErrorMessage))
			: Enumerable.Empty<Error>();
	}
}