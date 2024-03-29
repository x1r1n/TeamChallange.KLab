using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLab.Application.User.Commands.UpdateUserImage
{
	public class UpdateUserImageCommandHandler : ICommandHandler<UpdateUserImageCommand, Result>
	{
		private readonly IIdentityService _identityService;
		private readonly IFileService _fileService;
		public UpdateUserImageCommandHandler(IIdentityService identityService, IFileService fileService)
		{
			_identityService = identityService;
			_fileService = fileService;
		}

		public async Task<Result> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExistsAsync(request.Id);

			if (!isUserExists)
			{
				return Result.Failure(DomainErrors.User.NotFound);
			}

			var updateResult = await _fileService.UpdateImageAsync(request.Id, request.Image);

			return updateResult.IsSuccess
				? Result.Success()
				: Result.Failure(updateResult.Errors);
		}
	}
}
