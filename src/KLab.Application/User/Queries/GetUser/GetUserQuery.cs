using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Queries.GetUser
{
	/// <summary>
	/// Represents a query for retrieving a user information
	/// </summary>
	public class GetUserQuery : IQuery<Result<GetUserQueryResponse>>
	{
		public string? UserId { get; init; }

		public GetUserQuery(string userId)
		{
			UserId = userId;
		}
	}
}