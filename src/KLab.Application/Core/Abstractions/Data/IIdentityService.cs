using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;
using KLab.Domain.Entities;

namespace KLab.Application.Core.Abstractions.Data
{
	public interface IIdentityService
	{
		Task<Result<ApplicationUser>> FindUserAsync(string request, FindType type);
		Task<Result> CreateUserAsync(ApplicationUser request);
		Task<string> GenerateEmailVerificationTokenAsync(ApplicationUser user);
		Task<string> GenerateAuthenticationTokenAsync(ApplicationUser user);
		Task<Result> VerifyEmailAsync(ApplicationUser user, string token);
		Task<Result> AuthenticateUserAsync(ApplicationUser user, string token);
		Task<Result> IsEmailUniqueAsync(string email);
		Task<Result> IsEmailVerifiedAsync(ApplicationUser user);
	}
}