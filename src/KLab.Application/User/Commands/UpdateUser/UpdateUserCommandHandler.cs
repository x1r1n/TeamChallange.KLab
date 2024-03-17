using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.UpdateUser
{
	public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public UpdateUserCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExists(request.Id!);

			if (!isUserExists)
			{
				return Result.Failure(DomainErrors.User.NotFound);
			}

			var updateResult = await _identityService.UpdateUserAsync(request, cancellationToken);

			if (updateResult.isFailure)
			{
				return updateResult;
			}

			return Result.Success();
		}
	}
}
