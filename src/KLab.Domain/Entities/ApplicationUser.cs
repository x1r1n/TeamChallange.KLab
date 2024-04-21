using Microsoft.AspNetCore.Identity;

namespace KLab.Domain.Entities
{
	/// <summary>
	/// Represents an application user entity
	/// </summary>
	public class ApplicationUser : IdentityUser
	{
		public string? Nickname { get; set; }
		public string? Description { get; set; }
		public DateTimeOffset CreatedAtUtc { get; private set; }
		public List<ChatUsers>? Chats { get; set; }
		public List<Messages>? Messages { get; set; }

		public ApplicationUser()
		{
			CreatedAtUtc = DateTimeOffset.UtcNow;
		}
	}
}