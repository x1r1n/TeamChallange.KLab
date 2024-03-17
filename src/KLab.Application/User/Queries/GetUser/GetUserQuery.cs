using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Queries.GetUser
{
	public class GetUserQuery : IQuery<Result<GetUserQueryResponse>>
	{
		public string? UserName { get; init; }

		public GetUserQuery(string username)
		{
			UserName = username;
		}
	}
}