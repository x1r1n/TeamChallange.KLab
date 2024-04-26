using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Messaging;
using KLab.Application.Core.Errors;
using KLab.Domain.Core.Enums;
using KLab.Domain.Core.Primitives;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Dashboard.AssignRole
{
	/// <summary>
	/// Represents a handler of AssignRoleCommand to assign role
	/// </summary>
	public class AssignRoleCommandHandler : ICommandHandler<AssignRoleCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public AssignRoleCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		/// <summary>
		/// Handles an assigning role and removing old one
		/// </summary>
		/// <param name="request">The user id and role to be assigned</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The result of the operation</returns>
		public async Task<Result> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
		{
			var findResult = await _identityService.FindUserAsync(request.Id, FindType.Id);

			if (findResult.isFailure)
			{
				return Result.Failure(findResult.Errors);
			}

			var getRoleResult = GetRole(request.RoleName.ToLower());

			if (getRoleResult.isFailure)
			{
				return Result.Failure(getRoleResult.Errors);
			}

			var assignRoleResult = await _identityService.AssignRoleAsync(findResult.Value, getRoleResult.Value);

			return assignRoleResult.IsSuccess
				? Result.Success()
				: Result.Failure(assignRoleResult.Errors);
		}

		private Result<Roles> GetRole(string roleName) =>
			roleName switch
			{
				"user" => Result.Success(Roles.User),
				"moderator" => Result.Success(Roles.Moderator),
				"administrator" => Result.Success(Roles.Administrator),
				_ => Result.Failure<Roles>(ValidationErrors.Dashboard.IncorrectRole)
			};
	}
}
