using KLab.Application.Core.Abstractions.Data;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;
using KLab.Domain.Entities;
using KLab.Infrastructure.Core.Extensions;
using KLab.Infrastructure.Core.Provider;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KLab.Infrastructure.Core.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result> AuthenticateUserAsync(ApplicationUser user, string token)
        {
            var correctCode = await _userManager
                .VerifyUserTokenAsync(
                user,
                tokenProvider: nameof(CustomEmailTokenProvider),
                purpose: "Authentication",
                token);

            if (!correctCode)
            {
                return Result.Failure(DomainErrors.Authentication.IncorrectAuthenticationCode);
            }

            await _signInManager.SignInAsync(user, true);

            return Result.Success();
        }

        public async Task<Result> CreateUserAsync(ApplicationUser request)
        {
            var creationResult = await _userManager.CreateAsync(request);

            if (!creationResult.Succeeded)
            {
                return Result.Failure(creationResult.Errors.ToDomainErrors());
            }

            return Result.Success();
        }

        public async Task<Result<ApplicationUser>> FindUserAsync(string request, FindType type)
        {
            var user = await _userManager.FindAsync(request, type);

            if (user is null)
            {
                return Result.Failure<ApplicationUser>(DomainErrors.User.NotFound);
            }

            return Result.Success(user);
        }

        public async Task<string> GenerateAuthenticationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateUserTokenAsync(
                user,
                tokenProvider: nameof(CustomEmailTokenProvider),
                purpose: "Authentication");

            return token;
        }

        public async Task<string> GenerateEmailVerificationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return token;
        }

        public async Task<Result> IsEmailUniqueAsync(string email)
        {
            var userExist = await _userManager.Users.AnyAsync(u => u.Email == email);

            if (userExist)
            {
                return Result.Failure(DomainErrors.User.AlreadyRegistered);
            }

            return Result.Success();
        }

        public async Task<Result> IsEmailVerifiedAsync(ApplicationUser user)
        {
            var emailVerified = await _userManager.IsEmailConfirmedAsync(user);

            if (!emailVerified)
            {
                return Result.Failure(DomainErrors.Authentication.UnverifiedEmail);
            }

            return Result.Success();
        }

        public async Task<Result> VerifyEmailAsync(ApplicationUser user, string token)
        {
            var confirmationResult = await _userManager.ConfirmEmailAsync(user, token);

            if (!confirmationResult.Succeeded)
            {
                return Result.Failure(confirmationResult.Errors.ToDomainErrors());
            }

            await _signInManager.SignInAsync(user, true);

            return Result.Success();
        }
    }
}