namespace KLab.Domain.Entities
{
	/// <summary>
	/// Represents a message entity
	/// </summary>
	public class Messages
	{
		private readonly DateTime _createdAt;

		public int Id { get; set; }
		public string? Content { get; set; }
		public DateTime CreatedAt => _createdAt;
		public string? SenderId { get; set; }
		public int ChatId { get; set; }

		public ApplicationUser? Sender { get; set; }
		public Chats? Chat { get; set; }

		public Messages()
		{
			_createdAt = DateTime.UtcNow;
		}
	}
}