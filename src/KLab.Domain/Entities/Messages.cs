namespace KLab.Domain.Entities
{
	public class Messages
	{
		private readonly DateTimeOffset _createdAt;

		public int Id { get; set; }
		public string? Content { get; set; }
		public DateTimeOffset CreatedAt => _createdAt;
		public string? SenderId { get; set; }
		public int ChatId { get; set; }

		public ApplicationUser? Sender { get; set; }
		public Chats? Chat { get; set; }

		public Messages()
		{
			_createdAt = DateTimeOffset.UtcNow;
		}
	}
}