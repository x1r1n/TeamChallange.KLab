using FluentValidation;
using KLab.Application.Core.Errors;
using KLab.Application.Core.Extensions;

namespace KLab.Application.Dashboard.AssignRole
{
    /// <summary>
    /// Represents a validator of AssignRoleCommand
    /// </summary>
	public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
	{
        /// <summary>
        /// Validates a user id and role name
        /// </summary>
        public AssignRoleCommandValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithError(ValidationErrors.User.IdIsRequired);

            RuleFor(request => request.RoleName)
                .NotEmpty()
                .WithError(ValidationErrors.Dashboard.RoleIsRequired);
        }
    }
}
