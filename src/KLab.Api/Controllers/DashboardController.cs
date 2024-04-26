using KLab.Api.Contracts;
using KLab.Api.Infrastructure;
using KLab.Application.Dashboard.AssignRole;
using KLab.Application.User.Commands.DeleteUser;
using KLab.Contracts.Dashboard;
using KLab.Domain.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLab.Api.Controllers
{
	/// <summary>
	/// The controller for managing dashboard-related actions 
    /// accessible to moderators and administrators 
	/// </summary>
	public class DashboardController : ApiController
	{
        public DashboardController(IMediator mediator)
            : base(mediator)
        {
        }

		/// <summary>
		/// Assigns a new role to a user identified by the specified id
		/// Requires administrator role
		/// </summary>
		/// <param name="id">The id of the user to whom the role will be assigned</param>
		/// <param name="request">The request containing the new role name</param>
		/// <response code="204">No Content: If the role assignment is successful</response>
		/// <response code="400">Bad Request: If the request is invalid</response>
		/// <response code="404">Not Found: If the user with the specified id is not found</response>
		/// <response code="422">Unprocessable Entity: If the role assignment process encounters validation errors</response>
		/// <response code="500">Internal Server Error: If an unexpected error occurs during processing</response>
		[HttpPatch(ApiRoutes.Dashboard.UserRoleManagement)]
        [Authorize(Roles = nameof(Roles.Administrator))]
		public async Task<IActionResult> AssignRole(string id, [FromBody] AssignRoleRequest request)
        {
            var result = await _mediator.Send(new AssignRoleCommand(id, request.RoleName!));

            return result.IsSuccess
                ? NoContent()
                : HandleFailure(result.Errors);
        }

		/// <summary>
		/// Deletes a user with the specified id from the application
		/// Requires administrator role
		/// </summary>
		/// <param name="id">The id of the user to be deleted</param>
		/// <response code="204">No Content: If the user is successfully deleted</response>
		/// <response code="400">Bad Request: If the request is invalid</response>
		/// <response code="404">Not Found: If the user with the specified id is not found</response>
		/// <response code="422">Unprocessable Entity: If the user deletion process encounters validation errors</response>
		/// <response code="500">Internal Server Error: If an unexpected error occurs during processing</response>
		[HttpDelete(ApiRoutes.Dashboard.UserManagement)]
		[Authorize(Roles = nameof(Roles.Administrator))]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var result = await _mediator.Send(new DeleteUserCommand(id));

			return result.IsSuccess
				? NoContent()
				: HandleFailure(result.Errors);
		}
    }
}
