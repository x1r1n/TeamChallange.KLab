using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.SignOut
{
	/// <summary>
	/// Represents a handler for sign out
	/// </summary>
	public class SignOutCommandHandler : ICommandHandler<SignOutCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public SignOutCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		/// <summary>
		///	Ending a user session and sign out of the application
		/// </summary>
		/// <param name="request">SignOutCommand</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of sign our</returns>
		public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
		{
			var result = await _identityService.SignOutAsync();

			return result.IsSuccess
				? Result.Success()
				: Result.Failure(result.Errors);
		}
	}
}