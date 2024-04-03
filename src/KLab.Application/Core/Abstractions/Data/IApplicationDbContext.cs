using KLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KLab.Application.Core.Abstractions.Data
{
	/// <summary>
	/// Represents the interface for the application's database context.
	/// </summary>
	public interface IApplicationDbContext
	{
		DbSet<Messages> Messages { get; set; }
		DbSet<Chats> Chats { get; set; }
		DbSet<ChatUsers> ChatUsers { get; set; }

		/// <summary>
		/// Save changes in database
		/// </summary>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of state entries written to the database.</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}