using KLab.Domain.Core.Primitives.ErrorModel;

namespace KLab.Domain.Core.Primitives.ResultModel
{
	public class Result<TValue> : Result
	{
		private readonly TValue _value;

		protected internal Result(TValue value, bool isSuccess, IEnumerable<Error> errors)
			: base(isSuccess, errors)
		{
			_value = value;
		}

		/// <summary>
		/// Gets the value of the result if it is successful; otherwise, throws an exception
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when attempting to access the value of a failure result</exception>
		public TValue Value => IsSuccess
			? _value
			: throw new InvalidOperationException("The value of a failure result can not be accessed.");

		/// <summary>
		/// Implicitly converts a value to a successful result
		/// </summary>
		/// <param name="value">The value to convert</param>
		/// <returns>A successful result containing the specified value</returns>
		public static implicit operator Result<TValue>(TValue value) => Success(value);
	}
}