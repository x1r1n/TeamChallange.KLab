using KLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KLab.Application.Core.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Messages> Messages { get; set; }
        DbSet<Chats> Chats { get; set; }
        DbSet<ChatUsers> ChatUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}