using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.User.Commands.DeleteUser
{
	/// <summary>
	/// Represents a command handler to delete user
	/// </summary>
	public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public DeleteUserCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		/// <summary>
		/// Handles a command of delete user by asynchronously locating and deleting the user
		/// </summary>
		/// <param name="request">The user id to be processed</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var findResult = await _identityService.FindUserAsync(request.Id, FindType.Id);

			if (findResult.isFailure)
			{
				return Result.Failure(findResult.Errors);
			}

			var deleteResult = await _identityService.DeleteUserAsync(findResult.Value);

			return deleteResult.IsSuccess
				? Result.Success()
				: Result.Failure(deleteResult.Errors);
		}
	}
}
