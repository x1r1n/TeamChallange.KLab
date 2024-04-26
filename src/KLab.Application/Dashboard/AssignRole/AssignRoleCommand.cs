using KLab.Application.Core.Abstractions.Messaging;
using KLab.Domain.Core.Enums;
using KLab.Domain.Core.Primitives.ResultModel;

namespace KLab.Application.Dashboard.AssignRole
{
	/// <summary>
	/// Represents a command for handler to assign role and remove old one
	/// </summary>
	public class AssignRoleCommand : ICommand<Result>
	{
		public string Id { get; init; }
		public string RoleName { get; init; }

		public AssignRoleCommand(string id, string role)
		{
			Id = id;
			RoleName = role;
		}
	}
}
