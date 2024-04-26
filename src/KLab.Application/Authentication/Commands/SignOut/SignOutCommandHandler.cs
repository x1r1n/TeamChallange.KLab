using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.SignOut
{
	/// <summary>
	/// Represents a command handler to sign out
	/// </summary>
	public class SignOutCommandHandler : ICommandHandler<SignOutCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public SignOutCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		/// <summary>
		///	Signs out the currently authenticated user
		/// </summary>
		/// <param name="request">The command to sign out the currently authenticated user</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
		{
			var result = await _identityService.SignOutAsync();

			return result.IsSuccess
				? Result.Success()
				: Result.Failure(result.Errors);
		}
	}
}