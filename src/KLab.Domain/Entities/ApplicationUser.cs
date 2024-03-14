using Microsoft.AspNetCore.Identity;

namespace KLab.Domain.Entities
{
	public class ApplicationUser : IdentityUser
	{
		private readonly DateTimeOffset? _createdAt;

		public string? FullName { get; set; }
		public string? AvatarUrl { get; set; }
		public DateTimeOffset? CreatedAt => _createdAt;
		public List<ChatUsers>? Chats { get; set; }
		public List<Messages>? Messages { get; set; }

		public ApplicationUser()
		{
			_createdAt = DateTimeOffset.UtcNow;
		}
	}
}