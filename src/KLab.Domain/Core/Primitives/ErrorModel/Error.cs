namespace KLab.Domain.Core.Primitives.ErrorModel
{
	/// <summary>
	/// Represents an error
	/// </summary>
	public class Error
	{
		public string Code { get; init; }
		public string Description { get; init; }
		public ErrorType Type { get; init; }

		/// <summary>
		/// Represents a none error
		/// </summary>
		public static Error None => new Error(string.Empty, string.Empty, ErrorType.None);

		private Error(string code, string description, ErrorType type)
		{
			Code = code;
			Description = description;
			Type = type;
		}

		/// <summary>
		/// Creates an internal failure error
		/// </summary>
		/// <param name="code">The error code</param>
		/// <param name="description">The error description</param>
		/// <returns>The failure error</returns>
		public static Error InternalFailure(string code, string description) =>
			new(code, description, ErrorType.InternalFailure);

		/// <summary>
		/// Creates a failure error
		/// </summary>
		/// <param name="code">The error code</param>
		/// <param name="description">The error description</param>
		/// <returns>The failure error</returns>
		public static Error Failure(string code, string description) =>
			new(code, description, ErrorType.Failure);

		/// <summary>
		/// Creates a forbidden error
		/// </summary>
		/// <param name="code">The error code</param>
		/// <param name="description">The error description</param>
		/// <returns>The forbidden error</returns>
		public static Error Forbidden(string code, string description) =>
			new(code, description, ErrorType.Forbidden);

		/// <summary>
		/// Creates a validation error
		/// </summary>
		/// <param name="code">The error code</param>
		/// <param name="description">The error description</param>
		/// <returns>The validation error</returns>
		public static Error Validition(string code, string description) =>
			new(code, description, ErrorType.Validition);

		/// <summary>
		/// Creates a not found error
		/// </summary>
		/// <param name="code">The error code</param>
		/// <param name="description">The error description</param>
		/// <returns>The not found error</returns>
		public static Error NotFound(string code, string description) =>
			new(code, description, ErrorType.NotFound);

		/// <summary>
		/// Creates a conflict error
		/// </summary>
		/// <param name="code">The error code</param>
		/// <param name="description">The error description</param>
		/// <returns>The conflict error</returns>
		public static Error Conflict(string code, string description) =>
			new(code, description, ErrorType.Conflict);

		/// <summary>
		/// Implicitly converts an error to its code
		/// </summary>
		/// <param name="error">The error</param>
		/// <returns>The error code</returns>
		public static implicit operator string(Error error) => error?.Code ?? string.Empty;
	}
}