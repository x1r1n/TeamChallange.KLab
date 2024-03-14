using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;
using KLab.Domain.Core.Constants.Regex;

namespace KLab.Application.Authentication.Commands.ConfirmEmail
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(request => request.Email)
                .Cascade(CascadeMode.Continue)
                .NotEmpty()
                .WithError(ValidationErrors.Authentication.EmailIsRequired)
                .EmailAddress()
                .WithError(ValidationErrors.Authentication.EmailIsNotValid);

            RuleFor(request => request.VerificationCode)
                .NotEmpty()
                .WithError(ValidationErrors.Authentication.VerificationCodeIsRequired);
        }
    }
}