using KLab.Domain.Core.Primitives.ErrorModel;
using System.Collections.ObjectModel;

namespace KLab.Domain.Core.Primitives.ResultModel
{
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

		public static Result Success() => new(true, Enumerable.Empty<Error>());

        public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Enumerable.Empty<Error>());

        public static Result Failure(Error error) => new(false, new Collection<Error> { error });

        public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default!, false, new Collection<Error> { error });

		public static Result Failure(IEnumerable<Error> errors) => new(false, errors);

		public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) => new Result<TValue>(default!, false, errors);
	}
}