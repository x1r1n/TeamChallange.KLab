namespace KLab.Domain.Entities
{
	public class ChatUsers
	{
		public int ChatId { get; set; }
		public string? UserId { get; set; }

		public ApplicationUser? User { get; set; }
		public Chats? Chat { get; set; }
	}
}
