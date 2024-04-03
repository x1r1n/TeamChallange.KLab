using FluentValidation;
using KLab.Domain.Core.Primitives.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;

namespace KLab.Api.Infrastructure.Handlers
{
	/// <summary>
	/// The middleware for handle exception
	/// </summary>
	public class GlobalExceptionHandler : IExceptionHandler
	{
		/// <summary>
		/// Represents async method for handle exception
		/// </summary>
		/// <param name="httpContext">The HTTP context</param>
		/// <param name="exception">The exception to be handled</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>A bool value that represent the success or failure of handling</returns>
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

		/// <summary>
		/// Gets a status code based on the exception
		/// </summary>
		/// <param name="exception">The exception to be handled</param>
		/// <returns>A status code that corresponds to the exception</returns>
		private static int GetStatusCode(Exception exception) =>
			exception switch
			{
				ValidationException => StatusCodes.Status422UnprocessableEntity,
				_ => StatusCodes.Status500InternalServerError
			};

		/// <summary>
		/// Gets a details about exception
		/// </summary>
		/// <param name="exception">The exception to be handled</param>
		/// <returns>A details that corresponds to the exception</returns>
		private static string GetDetail(Exception exception) =>
			exception is ValidationException ? "Validation failed" : exception.Message;

		/// <summary>
		/// Gets collection of errors based on the specific exception or empty
		/// </summary>
		/// <param name="exception">The exception to be handled</param>
		/// <returns>A collection of errors</returns>
		private static IEnumerable<Error> GetErrors(Exception exception) =>
			exception is ValidationException validationException
			? validationException.Errors.Select(error => Error.Validition(error.ErrorCode, error.ErrorMessage))
			: Enumerable.Empty<Error>();
	}
}