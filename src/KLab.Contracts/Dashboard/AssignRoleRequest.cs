using System.ComponentModel.DataAnnotations;

namespace KLab.Contracts.Dashboard
{
	/// <summary>
	/// Represents a request to assign role
	/// </summary>
	public class AssignRoleRequest
	{
		[Required]
		public string? RoleName { get; set; }
	}
}
