using FluentValidation;
using FluentValidation.Results;
using KLab.Application.Core.Abstractions.Messaging;
using MediatR;

namespace KLab.Application.Core.Behaviours
{
	/// <summary>
	/// Represents a validation behavior
	/// </summary>
	/// <typeparam name="TRequest">The type of request</typeparam>
	/// <typeparam name="TResponse">The type of response</typeparam>
	internal sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : class, IRequest<TResponse>
		where TResponse : class
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		/// <summary>
		/// Handles the validation of requests in the pipeline
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
			if (request is IQuery<TResponse>)
			{
				return await next();
			}

			var context = new ValidationContext<TRequest>(request);

			List<ValidationFailure> failures = _validators
				.Select(validator => validator.Validate(context))
				.SelectMany(result => result.Errors)
				.Where(failure => failure != null)
				.ToList();

			if (failures.Count != 0)
			{
				throw new ValidationException(failures);
			}

			return await next();
		}
	}
}