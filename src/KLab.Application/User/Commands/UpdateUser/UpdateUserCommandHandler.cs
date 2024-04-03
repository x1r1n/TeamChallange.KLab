using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.UpdateUser
{
	/// <summary>
	/// Represents a command handler for updating a user
	/// </summary>
	public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public UpdateUserCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		/// <summary>
		/// Handles the update of a user
		/// </summary>
		/// <param name="request">The command to update the user</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var isUserExists = await _identityService.IsUserExistsAsync(request.Id!);

			if (!isUserExists)
			{
				return Result.Failure(DomainErrors.User.NotFound);
			}

			var updateResult = await _identityService.UpdateUserAsync(request, cancellationToken);

			return updateResult.IsSuccess 
				? Result.Success()
				: Result.Failure(updateResult.Errors);
		}
	}
}
