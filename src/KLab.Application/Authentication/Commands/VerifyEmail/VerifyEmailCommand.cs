using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Authentication.Commands.ConfirmEmail
{
    public class VerifyEmailCommand : ICommand<Result>
    {
        public string? Email { get; init; }
        public string? VerificationCode { get; init; }

        public VerifyEmailCommand(string email, string verificationCode)
        {
            Email = email;
            VerificationCode = verificationCode;
        }
    }
}