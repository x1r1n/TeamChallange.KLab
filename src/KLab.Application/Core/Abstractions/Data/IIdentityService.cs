using KLab.Application.User.Commands.UpdateUser;
using KLab.Application.User.Queries.GetUser;
using KLab.Domain.Core.Enums;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;
using KLab.Domain.Entities;

namespace KLab.Application.Core.Abstractions.Data
{
    /// <summary>
    /// Represents the interface for the identity service
    /// </summary>
    public interface IIdentityService
	{
		/// <summary>
		/// Finds a user asynchronously based on the specified criteria
		/// </summary>
		/// <param name="request">The search criteria</param>
		/// <param name="type">The type of criteria</param>
		/// <returns>The result containing the found user information</returns>
		Task<Result<ApplicationUser>> FindUserAsync(string request, FindType type);

		/// <summary>
		/// Retrieves a user asynchronously based on the specified username
		/// </summary>
		/// <param name="username">The username</param>
		/// <returns>The result containing the user information</returns>
		Task<Result<GetUserQueryResponse>> GetUserAsync(string username);

		/// <summary>
		/// Creates a new user asynchronously
		/// </summary>
		/// <param name="request">The user information to create</param>
		/// <returns>The result of the operation</returns>
		Task<Result> CreateUserAsync(ApplicationUser request);

		/// <summary>
		/// Updates a user asynchronously
		/// </summary>
		/// <param name="request">The command that represent updatable data</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		Task<Result> UpdateUserAsync(UpdateUserCommand request, CancellationToken cancellationToken);

		/// <summary>
		/// Generates an email verification token asynchronously for the specified user
		/// </summary>
		/// <param name="user">The user for whom the token will be generated</param>
		/// <returns>The generated email verification token</returns>
		Task<string> GenerateEmailVerificationTokenAsync(ApplicationUser user);

		/// <summary>
		/// Generates an authentication token asynchronously for the specified user
		/// </summary>
		/// <param name="user">The user for whom the token will be generated</param>
		/// <returns>The generated authentication token</returns>
		Task<string> GenerateAuthenticationTokenAsync(ApplicationUser user);

		/// <summary>
		/// Verifies the email asynchronously for the specified user using the provided token
		/// </summary>
		/// <param name="user">The user whose email is to be verified</param>
		/// <param name="token">The token used for email verification</param>
		/// <returns>The result of the operation</returns>
		Task<Result> VerifyEmailAsync(ApplicationUser user, string token);

		/// <summary>
		/// Authenticates the user asynchronously using the provided token
		/// </summary>
		/// <param name="user">The user to authenticate</param>
		/// <param name="token">The authentication token</param>
		/// <returns>The result of the authentication operation</returns>
		Task<Result> AuthenticateUserAsync(ApplicationUser user, string token);

		/// <summary>
		/// Checks asynchronously if a user exists
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>A boolean indicating whether the user exists</returns>
		Task<bool> IsUserExistsAsync(string id);

		/// <summary>
		/// Checks asynchronously if an email is unique
		/// </summary>
		/// <param name="email">The email to check</param>
		/// <returns>The result of the uniqueness check</returns>
		Task<Result> IsEmailUniqueAsync(string email);

		/// <summary>
		/// Signs out the user asynchronously
		/// </summary>
		/// <returns>The result of the sign out operation</returns>
		Task<Result> SignOutAsync();

		/// <summary>
		/// Assigns a role to a user by specified identifier
		/// </summary>
		/// <param name="user">The user to whom the role is assigned</param>
		/// <param name="role">The role to assign</param>
		/// <returns>The result of assigning role</returns>
		Task<Result> AssignRoleAsync(ApplicationUser user, Roles role);

		/// <summary>
		/// Deletes the specified user from the application
		/// </summary>
		/// <param name="user">The user to be deleted</param>
		/// <returns>The result of deleting user</returns>
		Task<Result> DeleteUserAsync(ApplicationUser user);
	}
}