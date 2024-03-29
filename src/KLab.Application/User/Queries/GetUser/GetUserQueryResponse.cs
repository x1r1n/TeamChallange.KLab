namespace KLab.Application.User.Queries.GetUser
{
	public class GetUserQueryResponse
	{
		public string? Id { get; init; }
		public string? Username { get; init; }
		public string? Nickname { get; init; }
		public string? Email { get; init; }

		public GetUserQueryResponse(string id, string username, string nickname, string email)
		{
			Id = id;
			Username = username;
			Nickname = nickname;
			Email = email;
		}
	}
}
