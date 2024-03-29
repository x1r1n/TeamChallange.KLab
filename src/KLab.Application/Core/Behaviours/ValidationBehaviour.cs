using FluentValidation;
using FluentValidation.Results;
using KLab.Application.Core.Abstractions.Messaging;
using MediatR;

namespace KLab.Application.Core.Behaviours
{
	internal sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : class, IRequest<TResponse>
		where TResponse : class
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

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