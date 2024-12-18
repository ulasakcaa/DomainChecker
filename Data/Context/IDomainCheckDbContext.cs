using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Common;

namespace Data.Context
{
    public interface IDomainCheckDbContext : IAsyncDisposable
    {
        DbSet<T> Set<T>() where T : BaseContextEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
