using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Queries.GetUser
{
	/// <summary>
	/// Represents a command handler for retrieving a user information.
	/// </summary>
	public class GetUserQueryHandler : IQueryHandler<GetUserQuery, Result<GetUserQueryResponse>>
	{
		private readonly IIdentityService _identityService;

		public GetUserQueryHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		/// <summary>
		/// Handles the command to retreive user information
		/// </summary>
		/// <param name="request">The query to retrieve the user</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result containing the user information</returns>
		public async Task<Result<GetUserQueryResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			var result = await _identityService.GetUserAsync(request.UserId!);

			if (result.isFailure)
			{
				return Result.Failure<GetUserQueryResponse>(result.Errors);
			}

			return result;
		}
	}
}
