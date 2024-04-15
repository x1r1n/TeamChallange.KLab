namespace KLab.Application.User.Queries.GetUser
{
	/// <summary>
	/// Represents the response for retrieving user information.
	/// </summary>
	public class GetUserQueryResponse
	{
		public string? Id { get; init; }
		public string? Username { get; init; }
		public string? Nickname { get; init; }
		public string? Email { get; init; }
		public DateTimeOffset? CreatedAtUtc { get; init; }

		public GetUserQueryResponse(
			string id, 
			string username, 
			string nickname, 
			string email, 
			DateTimeOffset? createdAtUtc)
		{
			Id = id;
			Username = username;
			Nickname = nickname;
			Email = email;
			CreatedAtUtc = createdAtUtc;
		}
	}
}
