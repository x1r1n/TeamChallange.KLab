using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Queries.GetUserImage
{
	public class GetUserImageQueryHandler : IQueryHandler<GetUserImageQuery, Result<GetUserImageQueryResponse>>
	{
		private readonly IIdentityService _identityService;
		private readonly IFileService _fileService;

		public GetUserImageQueryHandler(IIdentityService identityService, IFileService fileService)
		{
			_identityService = identityService;
			_fileService = fileService;
		}

		public async Task<Result<GetUserImageQueryResponse>> Handle(GetUserImageQuery request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExistsAsync(request.Id);

			if (!isUserExists)
			{
				return Result.Failure<GetUserImageQueryResponse>(DomainErrors.User.NotFound);
			}

			var getImageResult = await _fileService.GetImageAsync(request.Id);

			return getImageResult.IsSuccess
				? Result.Success(getImageResult.Value)
				: Result.Failure<GetUserImageQueryResponse>(getImageResult.Errors);
		}
	}
}
