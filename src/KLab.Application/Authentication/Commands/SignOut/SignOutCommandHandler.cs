using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.SignOut
{
	public class SignOutCommandHandler : ICommandHandler<SignOutCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public SignOutCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
		{
			var result = await _identityService.SignOutAsync();

			if (result.isFailure)
			{
				return Result.Failure(result.Errors);
			}

			return Result.Success();
		}
	}
}