using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.ConfirmEmail
{
    public class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public VerifyEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var foundResult = await _identityService.FindUserAsync(request.Email!, FindType.Email);

            if (foundResult.isFailure)
            {
                return Result.Failure(foundResult.Error);
            }

            var user = foundResult.Value;

            var emailVerified = await _identityService.IsEmailVerifiedAsync(user);

            if (emailVerified.IsSuccess)
            {
                return Result.Failure(emailVerified.Error);
            }

            var confirmedResult = await _identityService.VerifyEmailAsync(user, request.VerificationCode!);

            if (confirmedResult.isFailure)
            {
                return Result.Failure(confirmedResult.Error);
            }

            return Result.Success();
        }
    }
}