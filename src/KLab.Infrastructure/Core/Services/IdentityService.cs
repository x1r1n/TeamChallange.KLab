using KLab.Application.Core.Abstractions.Data;
using KLab.Application.User.Commands.UpdateUser;
using KLab.Application.User.Queries.GetUser;
using KLab.Domain.Core.Enums;
using KLab.Domain.Core.Errors;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;
using KLab.Domain.Entities;
using KLab.Infrastructure.Core.Extensions;
using KLab.Infrastructure.Core.Provider;
using KLab.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KLab.Infrastructure.Core.Services
{
	/// <summary>
	/// Represents the interface for the identity service
	/// </summary>
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _context;

		public IdentityService(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager,
			ApplicationDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_context = context;
		}

		/// <summary>
		/// Assigns a role to a user by specified identifier
		/// </summary>
		/// <param name="user">The user to whom the role is assigned</param>
		/// <param name="role">The role to assign</param>
		/// <returns>The result of assign role</returns>
		public async Task<Result> AssignRoleAsync(ApplicationUser user, Roles role)
		{
			var roleName = role.ToString();
			var roleExists = await _roleManager.RoleExistsAsync(roleName);

			if (!roleExists)
			{
				await _roleManager.CreateAsync(new IdentityRole(roleName));
			}

			var removeRoleName = await _userManager.GetRolesAsync(user);
			var assignRoleResult = await _userManager.AddToRoleAsync(user, roleName);

			if (!assignRoleResult.Succeeded)
			{
				return Result.Failure(assignRoleResult.Errors.ToDomainErrors());
			}

			var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, removeRoleName.FirstOrDefault()!);

			if (!removeRoleResult.Succeeded)
			{
				await _userManager.RemoveFromRoleAsync(user, roleName);

				return Result.Failure(removeRoleResult.Errors.ToDomainErrors());
			}

			return Result.Success();
		}

		/// <summary>
		/// Authenticates the user asynchronously using the provided token
		/// </summary>
		/// <param name="user">The user to authenticate</param>
		/// <param name="token">The authentication token</param>
		/// <returns>The result of the authentication operation</returns>
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

		/// <summary>
		/// Creates a new user and assigns user role
		/// </summary>
		/// <param name="request">The user information to create</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> CreateUserAsync(ApplicationUser request)
		{
			var creationResult = await _userManager.CreateAsync(request);

			if (!creationResult.Succeeded)
			{
				return Result.Failure(creationResult.Errors.ToDomainErrors());
			}

			var roleExists = await _roleManager.RoleExistsAsync(nameof(Roles.User));

			if (!roleExists)
			{
				await _roleManager.CreateAsync(new IdentityRole(nameof(Roles.User)));
			}

			var assignRoleResult = await _userManager.AddToRoleAsync(request, nameof(Roles.User));

			if (!assignRoleResult.Succeeded)
			{
				await _userManager.DeleteAsync(request);

				return Result.Failure(assignRoleResult.Errors.ToDomainErrors());
			}

			return Result.Success();
		}

		/// <summary>
		/// Deletes the specified user from the application
		/// </summary>
		/// <param name="user">The user to be deleted</param>
		/// <returns>The result of deleting user</returns>
		public async Task<Result> DeleteUserAsync(ApplicationUser user)
		{
			var deleteResult = await _userManager.DeleteAsync(user);

			return deleteResult.Succeeded
				? Result.Success()
				: Result.Failure(deleteResult.Errors.ToDomainErrors());
		}

		/// <summary>
		/// Finds a user asynchronously based on the specified criteria
		/// </summary>
		/// <param name="request">The search criteria</param>
		/// <param name="type">The type of criteria</param>
		/// <returns>The result containing the found user information</returns>
		public async Task<Result<ApplicationUser>> FindUserAsync(string request, FindType type)
		{
			var user = await _userManager.FindAsync(request, type);

			if (user is null)
			{
				return Result.Failure<ApplicationUser>(DomainErrors.User.NotFound);
			}

			return Result.Success(user);
		}

		/// <summary>
		/// Generates an authentication token asynchronously for the specified user
		/// </summary>
		/// <param name="user">The user for whom the token will be generated</param>
		/// <returns>The generated authentication token</returns>
		public async Task<string> GenerateAuthenticationTokenAsync(ApplicationUser user)
		{
			var token = await _userManager.GenerateUserTokenAsync(
				user,
				tokenProvider: nameof(CustomEmailTokenProvider),
				purpose: "Authentication");

			return token;
		}

		/// <summary>
		/// Generates an email verification token asynchronously for the specified user
		/// </summary>
		/// <param name="user">The user for whom the token will be generated</param>
		/// <returns>The generated email verification token</returns>
		public async Task<string> GenerateEmailVerificationTokenAsync(ApplicationUser user)
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			return token;
		}

		/// <summary>
		/// Retrieves a user asynchronously based on the specified user identifier
		/// </summary>
		/// <param name="id">The user identifier</param>
		/// <returns>The result containing the user information</returns>
		public async Task<Result<GetUserQueryResponse>> GetUserAsync(string id)
		{
			var user = await _context.Users
				.AsNoTracking()
				.Where(user => user.Id == id)
				.Join(
				_context.UserRoles.AsNoTracking(),
				user => user.Id,
				userRole => userRole.UserId,
				(user, userRole) => new { User = user, UserRole = userRole })
				.Join(
				_context.Roles.AsNoTracking(),
				userRole => userRole.UserRole.RoleId,
				role => role.Id,
				(userRole, role) => new GetUserQueryResponse(
					userRole.User.Id!,
					userRole.User.UserName!,
					userRole.User.Nickname!,
					userRole.User.Email!,
					userRole.User.Description!,
					role.Name!,
					userRole.User.CreatedAtUtc))
				.FirstOrDefaultAsync();

			if (user == default)
			{
				return Result.Failure<GetUserQueryResponse>(DomainErrors.User.NotFound);
			}

			return Result.Success(user);
		}

		/// <summary>
		/// Checks asynchronously if an email is unique
		/// </summary>
		/// <param name="email">The email to check</param>
		/// <returns>The result of the uniqueness check</returns>
		public async Task<Result> IsEmailUniqueAsync(string email)
		{
			var userExist = await _userManager.Users.AnyAsync(u => u.Email == email);

			if (userExist)
			{
				return Result.Failure(DomainErrors.User.AlreadyRegistered);
			}

			return Result.Success();
		}

		/// <summary>
		/// Checks asynchronously if a user exists
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>A boolean indicating whether the user exists</returns>
		public async Task<bool> IsUserExistsAsync(string id)
		{
			return await _userManager.Users.AnyAsync(user => user.Id == id);
		}

		/// <summary>
		/// Signs out the user asynchronously
		/// </summary>
		/// <returns>The result of the sign out operation</returns>
		public async Task<Result> SignOutAsync()
		{
			await _signInManager.SignOutAsync();

			return Result.Success();
		}

		/// <summary>
		/// Updates a user asynchronously
		/// </summary>
		/// <param name="request">The command that represent updatable data</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> UpdateUserAsync(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			await _userManager.Users
				.Where(user => user.Id == request.Id)
				.ExecuteUpdateAsync(user => user
				.SetProperty(
					user => user.Nickname,
					user => request.Nickname != null ? request.Nickname : user.Nickname)
				.SetProperty(
					user => user.Description,
					user => request.Description != null ? request.Description : user.Description),
					cancellationToken);

			await _context.SaveChangesAsync();

			return Result.Success();
		}

		/// <summary>
		/// Verifies the email asynchronously for the specified user using the provided token
		/// </summary>
		/// <param name="user">The user whose email is to be verified</param>
		/// <param name="token">The token used for email verification</param>
		/// <returns>The result of the operation</returns>
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