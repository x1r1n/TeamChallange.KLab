using FluentValidation;
using KLab.Domain.Core.Primitives.ErrorModel;

namespace KLab.Application.Core.Extensions
{
	/// <summary>
	/// Represents extension methods for FluentValidation
	/// </summary>
	public static class FluentValidationExtensions
	{
		/// <summary>
		/// Adds an error to the validation rule
		/// </summary>
		/// <typeparam name="T">The type of object being validated</typeparam>
		/// <typeparam name="TProperty">The type of property being validated</typeparam>
		/// <param name="rule">The rule builder options</param>
		/// <param name="error">The error to be added</param>
		/// <returns>The rule builder options</returns>
		/// <exception cref="ArgumentNullException">Thrown when the error is null</exception>
		public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
			this IRuleBuilderOptions<T, TProperty> rule, Error error)
		{
			if (error is null)
			{
				throw new ArgumentNullException(nameof(error), "The error is required.");
			}

			return rule
				.WithErrorCode(error.Code)
				.WithMessage(error.Description);
		}
	}
}