namespace KLab.Domain.Core.Primitives.ErrorModel
{
	/// <summary>
	/// Represents the type of an error
	/// </summary>
	public enum ErrorType
	{
		None = 0,
		Failure = 1,
		Validition = 2,
		NotFound = 3,
		Conflict = 4
	}
}