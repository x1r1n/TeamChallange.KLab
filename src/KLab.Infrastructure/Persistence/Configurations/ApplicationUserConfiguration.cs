using KLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KLab.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasMany(user => user.Chats)
                .WithOne(chat => chat.User)
                .HasForeignKey(chat => chat.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(user => user.Messages)
                .WithOne(message => message.Sender)
                .HasForeignKey(message => message.SenderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}