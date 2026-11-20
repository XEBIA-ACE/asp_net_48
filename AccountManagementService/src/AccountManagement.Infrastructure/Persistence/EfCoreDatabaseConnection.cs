using AccountManagement.Application.Ports;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.Persistence;

/// <summary>
/// EF Core adapter for <see cref="IDatabaseConnection"/>.
/// </summary>
public sealed class EfCoreDatabaseConnection(IDbContextFactory<AccountManagementDbContext> contextFactory)
    : IDatabaseConnection
{
    public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);
        return await context.Database.CanConnectAsync(cancellationToken).ConfigureAwait(false);
    }
}
