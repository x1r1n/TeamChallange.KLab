using Microsoft.AspNetCore.Identity;

namespace KLab.Domain.Entities
{
	/// <summary>
	/// Represents an application user entity
	/// </summary>
	public class ApplicationUser : IdentityUser
	{
		private readonly DateTime _createdAt;

		public string? Nickname { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt => _createdAt;
		public List<ChatUsers>? Chats { get; set; }
		public List<Messages>? Messages { get; set; }

		public ApplicationUser()
		{
			_createdAt = DateTime.UtcNow;
		}
	}
}