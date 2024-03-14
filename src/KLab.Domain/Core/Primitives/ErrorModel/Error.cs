namespace KLab.Domain.Core.Primitives.ErrorModel
{
    public class Error
    {
        public string Code { get; init; }
        public string Description { get; init; }
        public ErrorType Type { get; init; }
        public static Error None => new Error(string.Empty, string.Empty, ErrorType.None);

        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        public static Error Failure(string code, string description) =>
            new(code, description, ErrorType.Failure);

		public static Error Validition(string code, string description) =>
			new(code, description, ErrorType.Validition);

		public static Error NotFound(string code, string description) =>
			new(code, description, ErrorType.NotFound);

		public static Error Conflict(string code, string description) =>
			new(code, description, ErrorType.Conflict);

		public static implicit operator string(Error error) => error?.Code ?? string.Empty;
    }
}