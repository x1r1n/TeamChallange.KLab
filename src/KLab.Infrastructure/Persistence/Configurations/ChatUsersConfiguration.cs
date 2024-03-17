﻿using KLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KLab.Infrastructure.Persistence.Configurations
{
	public class ChatUsersConfiguration : IEntityTypeConfiguration<ChatUsers>
	{
		public void Configure(EntityTypeBuilder<ChatUsers> builder)
		{
			builder.HasKey(cu => new
			{
				cu.ChatId,
				cu.UserId
			});
		}
	}
}