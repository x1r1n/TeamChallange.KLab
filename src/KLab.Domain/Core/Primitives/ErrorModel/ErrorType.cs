namespace KLab.Domain.Core.Primitives.ErrorModel
{
	/// <summary>
	/// Represents the type of an error
	/// </summary>
	public enum ErrorType
	{
		None = 200,
		Failure = 400,
		Forbidden = 403,
		NotFound = 404,
		Conflict = 409,
		Validition = 422,
		InternalFailure = 500
	}
}