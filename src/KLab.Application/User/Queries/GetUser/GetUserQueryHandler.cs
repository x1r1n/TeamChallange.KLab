using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Queries.GetUser
{
	public class GetUserQueryHandler : IQueryHandler<GetUserQuery, Result<GetUserQueryResponse>>
	{
		private readonly IIdentityService _identityService;

		public GetUserQueryHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public async Task<Result<GetUserQueryResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			var result = await _identityService.GetUserAsync(request.UserName!);

			if (result.isFailure)
			{
				return Result.Failure<GetUserQueryResponse>(result.Errors);
			}

			return result;
		}
	}
}
