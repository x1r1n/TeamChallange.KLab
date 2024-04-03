using KLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KLab.Infrastructure.Persistence.Configurations
{
	/// <summary>
	/// Configures the entity type for Chats
	/// </summary>
	public class ChatsConfiguration : IEntityTypeConfiguration<Chats>
	{
		public void Configure(EntityTypeBuilder<Chats> builder)
		{
			builder.HasKey(chat => chat.Id);

			builder
				.HasMany(chat => chat.Users)
				.WithOne(user => user.Chat)
				.HasForeignKey(chat => chat.ChatId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasMany(chat => chat.Messages)
				.WithOne(message => message.Chat)
				.HasForeignKey(message => message.ChatId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}