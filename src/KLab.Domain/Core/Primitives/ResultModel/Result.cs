using KLab.Domain.Core.Primitives.ErrorModel;
using System.Collections.ObjectModel;

namespace KLab.Domain.Core.Primitives.ResultModel
{
	/// <summary>
	/// Represents a result
	/// </summary>
	public class Result
	{
		public bool IsSuccess { get; }
		public bool isFailure => !IsSuccess;
		public IEnumerable<Error> Errors { get; }

		protected Result(bool isSuccess, IEnumerable<Error> errors)
		{
			IsSuccess = isSuccess;
			Errors = errors;
		}

		/// <summary>
		/// Creates a successful result
		/// </summary>
		/// <returns>The successful result</returns>
		public static Result Success() => new(true, Enumerable.Empty<Error>());

		/// <summary>
		/// Creates a successful result with a value
		/// </summary>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="value">The value</param>
		/// <returns>The successful result with the specified value</returns>
		public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Enumerable.Empty<Error>());

		/// <summary>
		/// Creates a failure result with a single error
		/// </summary>
		/// <param name="error">The error</param>
		/// <returns>The failure result with the specified error</returns>
		public static Result Failure(Error error) => new(false, new Collection<Error> { error });

		/// <summary>
		/// Creates a failure result with a single error and a default value
		/// </summary>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="error">The error</param>
		/// <returns>The failure result with the specified error and a default value</returns>
		public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default!, false, new Collection<Error> { error });

		/// <summary>
		/// Creates a failure result with multiple errors
		/// </summary>
		/// <param name="errors">The collection of errors</param>
		/// <returns>The failure result with the specified errors</returns>
		public static Result Failure(IEnumerable<Error> errors) => new(false, errors);

		/// <summary>
		/// Creates a failure result with multiple errors and a default value
		/// </summary>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="errors">The collection of errors</param>
		/// <returns>The failure result with the specified errors and a default value</returns>
		public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) => new Result<TValue>(default!, false, errors);
	}
}