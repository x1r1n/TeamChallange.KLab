using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.Authenticate
{
	public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public AuthenticateCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public async Task<Result> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
		{
			var foundResult = await _identityService.FindUserAsync(request.Email!, FindType.Email);

            if (foundResult.isFailure)
            {
				return Result.Failure(foundResult.Errors);
            }

			var user = foundResult.Value;

			if (!user.EmailConfirmed)
			{
				return Result.Failure(DomainErrors.Authentication.UnverifiedEmail);
			}

			var authenticateResult = await _identityService.AuthenticateUserAsync(user, request.AuthenticationCode!);

			if (authenticateResult.isFailure)
			{
				return Result.Failure(authenticateResult.Errors);
			}

			return Result.Success();
        }
	}
}