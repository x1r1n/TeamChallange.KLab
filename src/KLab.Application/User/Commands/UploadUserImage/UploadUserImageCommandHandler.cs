using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.UploadUserImage
{
	internal class UploadUserImageCommandHandler : ICommandHandler<UploadUserImageCommand, Result>
	{
		private readonly IIdentityService _identityService;
		private readonly IFileService _fileService;

		public UploadUserImageCommandHandler(IIdentityService identityService, IFileService fileService)
		{
			_identityService = identityService;
			_fileService = fileService;
		}

		public async Task<Result> Handle(UploadUserImageCommand request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExistsAsync(request.Id);

			if (!isUserExists)
			{
				return Result.Failure(DomainErrors.User.NotFound);
			}

			var updateResult = await _fileService.UploadImageAsync(request.Id, request.Image);

			return updateResult.IsSuccess 
				? Result.Success() 
				: Result.Failure(updateResult.Errors);
		}
	}
}
