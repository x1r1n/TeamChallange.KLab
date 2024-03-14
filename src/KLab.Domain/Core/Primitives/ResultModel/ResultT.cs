using KLab.Domain.Core.Primitives.ErrorModel;

namespace KLab.Domain.Core.Primitives.ResultModel
{
    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        protected internal Result(TValue value, bool isSuccess, Error error, IEnumerable<Error> errors)
            : base(isSuccess, error, errors)
        {
            _value = value;
        }

        public static implicit operator Result<TValue>(TValue value) => Success(value);

        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");
    }
}