﻿namespace KLab.Domain.Entities
{
	/// <summary>
	/// Represents a chat entity
	/// </summary>
	public class Chats
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public DateTimeOffset CreatedAtUtc { get; private set; }

		public List<ChatUsers>? Users { get; set; }
		public List<Messages>? Messages { get; set; }

        public Chats()
        {
			CreatedAtUtc = DateTimeOffset.UtcNow;
		}
    }
}