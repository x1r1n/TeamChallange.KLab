using KLab.Application.Core.Abstractions.Data;
using KLab.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KLab.Infrastructure.Persistence
{
	/// <summary>
	/// Represents the application database context, extending IdentityDbContext for ApplicationUser.
	/// </summary>
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
	{
		public DbSet<Messages> Messages { get; set; }
		public DbSet<Chats> Chats { get; set; }
		public DbSet<ChatUsers> ChatUsers { get; set; }

		public ApplicationDbContext() { }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Configures the database context and entitys
		/// </summary>
		/// <param name="modelBuilder">The model builder</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ApplicationUser>().ToTable("Users");

			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");

			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

			modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

			modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

			modelBuilder.Entity<IdentityRole>().ToTable("Roles");

			modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

		}
	}
}