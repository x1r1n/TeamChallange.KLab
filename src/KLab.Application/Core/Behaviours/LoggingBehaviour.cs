using KLab.Domain.Core.Primitives.ResultModel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KLab.Application.Core.Behaviours
{
	public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : class, IRequest<TResponse>
		where TResponse : Result
	{
		private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
		public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

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
					result.Error,
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