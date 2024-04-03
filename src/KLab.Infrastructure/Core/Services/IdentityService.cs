using KLab.Application.Core.Abstractions.Data;
using KLab.Application.User.Commands.UpdateUser;
using KLab.Application.User.Queries.GetUser;
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
		private readonly ApplicationDbContext _context;

		public IdentityService(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			ApplicationDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
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
		/// Creates a new user asynchronously
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

			return Result.Success();
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
		/// Retrieves a user asynchronously based on the specified username
		/// </summary>
		/// <param name="username">The username</param>
		/// <returns>The result containing the user information</returns>
		public async Task<Result<GetUserQueryResponse>> GetUserAsync(string username)
		{
			var user = await _userManager.Users
				.AsNoTracking()
				.Where(user => user.UserName == username)
				.Select(user => new GetUserQueryResponse
				(
					user.Id,
					user.UserName!,
					user.Nickname!,
					user.Email!
				))
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
		/// <param name="userId">The user id</param>
		/// <returns>A boolean indicating whether the user exists</returns>
		public async Task<bool> IsUserExistsAsync(string userId)
		{
			return await _userManager.Users.AnyAsync(user => user.Id == userId);
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