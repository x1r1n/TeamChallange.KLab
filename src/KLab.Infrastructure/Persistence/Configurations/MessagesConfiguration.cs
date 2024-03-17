using KLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KLab.Infrastructure.Persistence.Configurations
{
	public class MessagesConfiguration : IEntityTypeConfiguration<Messages>
	{
		public void Configure(EntityTypeBuilder<Messages> builder)
		{
			builder.HasKey(message => message.Id);
		}
	}
}