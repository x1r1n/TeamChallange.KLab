namespace KLab.Domain.Entities
{
	/// <summary>
	/// Represents a message entity
	/// </summary>
	public class Messages
	{
		public int Id { get; set; }
		public string? Content { get; set; }
		public DateTimeOffset CreatedAtUtc {  get; private set; }
		public string? SenderId { get; set; }
		public int ChatId { get; set; }

		public ApplicationUser? Sender { get; set; }
		public Chats? Chat { get; set; }

		public Messages()
		{
			CreatedAtUtc = DateTimeOffset.UtcNow;
		}
	}
}