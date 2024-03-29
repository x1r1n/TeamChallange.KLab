using KLab.Application.User.Commands.UpdateUser;
using KLab.Application.User.Queries.GetUser;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;
using KLab.Domain.Entities;

namespace KLab.Application.Core.Abstractions.Data
{
	public interface IIdentityService
	{
		Task<Result<ApplicationUser>> FindUserAsync(string request, FindType type);
		Task<Result<GetUserQueryResponse>> GetUserAsync(string username);
		Task<Result> CreateUserAsync(ApplicationUser request);
		Task<Result> UpdateUserAsync(UpdateUserCommand request, CancellationToken cancellationToken);
		Task<string> GenerateEmailVerificationTokenAsync(ApplicationUser user);
		Task<string> GenerateAuthenticationTokenAsync(ApplicationUser user);
		Task<Result> VerifyEmailAsync(ApplicationUser user, string token);
		Task<Result> AuthenticateUserAsync(ApplicationUser user, string token);
		Task<bool> IsUserExistsAsync(string userId);
		Task<Result> IsEmailUniqueAsync(string email);
		Task<Result> SignOutAsync();
	}
}