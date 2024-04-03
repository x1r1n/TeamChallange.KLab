namespace KLab.Domain.Entities
{
	/// <summary>
	/// Represents the association between a user and a chat
	/// </summary>
	public class ChatUsers
	{
		public int ChatId { get; set; }
		public string? UserId { get; set; }

		public ApplicationUser? User { get; set; }
		public Chats? Chat { get; set; }
	}
}
