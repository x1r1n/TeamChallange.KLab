using KLab.Domain.Core.Primitives.ErrorModel;
using System.Collections.ObjectModel;

namespace KLab.Domain.Core.Primitives.ResultModel
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool isFailure => !IsSuccess;
        public Error Error { get; }
		public IEnumerable<Error> Errors { get; }

		protected Result(bool isSuccess, Error error, IEnumerable<Error> errors)
		{
			if (isSuccess && error.Type != ErrorType.None)
			{
				throw new InvalidOperationException();
			}
			else if (!isSuccess && error.Type == ErrorType.None)
			{
				throw new InvalidOperationException();
			}

			IsSuccess = isSuccess;
			Error = error;
			Errors = errors;
		}

		public static Result Success() => new(true, Error.None, default!);

        public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Error.None, Enumerable.Empty<Error>());

        public static Result Failure(Error error) => new(false, error, new Collection<Error>());

        public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default!, false, error, Enumerable.Empty<Error>());

		public static Result Failure(IEnumerable<Error> errors) => new(false, Error.None, errors);

		public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) => new Result<TValue>(default!, false, Error.None, errors);
	}
}