using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Queries.GetUserImage
{
	public class GetUserImageQuery : IQuery<Result<GetUserImageQueryResponse>>
	{
		public string Id { get; init; }

		public GetUserImageQuery(string id)
		{
			Id = id;
		}
	}
}
