namespace KLab.Contracts.User
{
	/// <summary>
	/// Represents a request for updating user information
	/// </summary>
	public class UpdateUserRequest
	{
		public string? Nickname { get; set; }
		public string? Description { get; set; }
	}
}
