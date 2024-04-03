using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.DeleteUserImage
{
	/// <summary>
	/// Represents a command handler for deleting a user's image
	/// </summary>
	public class DeleteUserImageCommandHandler : ICommandHandler<DeleteUserImageCommand, Result>
	{
		private readonly IIdentityService _identityService;
		private readonly IFileService _fileService;

		public DeleteUserImageCommandHandler(IIdentityService identityService, IFileService fileService)
		{
			_identityService = identityService;
			_fileService = fileService;
		}

		/// <summary>
		/// Handles the deletion of a user's image
		/// </summary>
		/// <param name="request">The command to delete the user's image</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> Handle(DeleteUserImageCommand request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExistsAsync(request.Id);

			if (!isUserExists)
			{
				return Result.Failure(DomainErrors.User.NotFound);
			}

			var deleteResult = await _fileService.DeleteUserImageAsync(request.Id);

			return deleteResult.IsSuccess
				? Result.Success()
				: Result.Failure(deleteResult.Errors);
		}
	}
}
