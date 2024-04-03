using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.UploadUserImage
{
	/// <summary>
	/// Represents a command handler for uploading a user's image
	/// </summary>
	public class UploadUserImageCommandHandler : ICommandHandler<UploadUserImageCommand, Result>
	{
		private readonly IIdentityService _identityService;
		private readonly IFileService _fileService;

		public UploadUserImageCommandHandler(IIdentityService identityService, IFileService fileService)
		{
			_identityService = identityService;
			_fileService = fileService;
		}

		/// <summary>
		/// Handles the command for uploading a user's image
		/// </summary>
		/// <param name="request">The command to upload the user's image</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> Handle(UploadUserImageCommand request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExistsAsync(request.Id);

			if (!isUserExists)
			{
				return Result.Failure(DomainErrors.User.NotFound);
			}

			var updateResult = await _fileService.UploadUserImageAsync(request.Id, request.Image);

			return updateResult.IsSuccess 
				? Result.Success() 
				: Result.Failure(updateResult.Errors);
		}
	}
}
