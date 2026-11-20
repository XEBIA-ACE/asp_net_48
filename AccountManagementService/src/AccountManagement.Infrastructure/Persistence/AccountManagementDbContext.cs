using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.Persistence;

/// <summary>
/// EF Core context for the service's PostgreSQL datastore. No entities are mapped yet.
/// </summary>
public sealed class AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options)
    : DbContext(options);
