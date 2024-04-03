using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Queries.GetUserImage
{
	/// <summary>
	/// Represents a command handler for retrieving a user's image
	/// </summary>
	public class GetUserImageQueryHandler : IQueryHandler<GetUserImageQuery, Result<GetUserImageQueryResponse>>
	{
		private readonly IIdentityService _identityService;
		private readonly IFileService _fileService;

		public GetUserImageQueryHandler(IIdentityService identityService, IFileService fileService)
		{
			_identityService = identityService;
			_fileService = fileService;
		}

		/// <summary>
		/// Handles the command to retrieve a user's image
		/// </summary>
		/// <param name="request">The query to retrieve the user's image</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result containing the user's image information</returns>
		public async Task<Result<GetUserImageQueryResponse>> Handle(GetUserImageQuery request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExistsAsync(request.Id);

			if (!isUserExists)
			{
				return Result.Failure<GetUserImageQueryResponse>(DomainErrors.User.NotFound);
			}

			var getImageResult = await _fileService.GetUserImageAsync(request.Id);

			return getImageResult.IsSuccess
				? Result.Success(getImageResult.Value)
				: Result.Failure<GetUserImageQueryResponse>(getImageResult.Errors);
		}
	}
}
