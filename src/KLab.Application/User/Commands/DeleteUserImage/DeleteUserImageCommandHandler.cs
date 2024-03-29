using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.DeleteUserImage
{
	public class DeleteUserImageCommandHandler : ICommandHandler<DeleteUserImageCommand, Result>
	{
		private readonly IIdentityService _identityService;
		private readonly IFileService _fileService;

		public DeleteUserImageCommandHandler(IIdentityService identityService, IFileService fileService)
		{
			_identityService = identityService;
			_fileService = fileService;
		}

		public async Task<Result> Handle(DeleteUserImageCommand request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExistsAsync(request.Id);

			if (!isUserExists)
			{
				return Result.Failure(DomainErrors.User.NotFound);
			}

			var deleteResult = await _fileService.DeleteImageAsync(request.Id);

			return deleteResult.IsSuccess
				? Result.Success()
				: Result.Failure(deleteResult.Errors);
		}
	}
}
