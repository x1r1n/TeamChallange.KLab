using KLab.Domain.Core.Primitives.ResultModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KLab.Application.Core.Behaviours
{
	/// <summary>
	/// Represents a logging behavior for requests and responses
	/// </summary>
	/// <typeparam name="TRequest">The type of request</typeparam>
	/// <typeparam name="TResponse">The type of response</typeparam>
	public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : class, IRequest<TResponse>
		where TResponse : Result
	{
		private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
		public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Handles logging for requests and responses in the pipeline
		/// </summary>
		/// <typeparam name="TRequest">The type of request</typeparam>
		/// <typeparam name="TResponse">The type of response</typeparam>
		/// <param name="request">The request being handled</param>
		/// <param name="next">The delegate to invoke the next handler in the pipeline</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The response from handling the request</returns>
		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			_logger.LogInformation(
				"Starting request {@RequestName}, {@DateTimeOffsetUtc}",
				typeof(TRequest).Name,
				DateTimeOffset.UtcNow);

			var result = await next();

			if (result.isFailure)
			{
				_logger.LogError(
					"Request failure {@RequestName}, {@Error}, {@DateTimeOffsetUtc}",
					typeof(TRequest).Name,
					result.Errors,
					DateTimeOffset.UtcNow);
			}

			_logger.LogInformation(
				"Completed request {@RequestName}, {@DateTimeOffsetUtc}",
				typeof(TRequest).Name,
				DateTimeOffset.UtcNow);

			return result;
		}
	}
}