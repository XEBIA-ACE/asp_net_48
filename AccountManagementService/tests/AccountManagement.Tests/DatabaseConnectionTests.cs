using AccountManagement.Application.Ports;
using AccountManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Tests;

[Collection(ServiceEnvironmentGroup.Name)]
public class DatabaseConnectionTests(ServiceEnvironmentFixture fixture)
{
    [Fact]
    public async Task DbContextFactory_IsConfiguredForPostgresFromEnvironment()
    {
        var contextFactory = fixture.Factory.Services.GetRequiredService<IDbContextFactory<AccountManagementDbContext>>();

        await using var context = await contextFactory.CreateDbContextAsync();

        Assert.Equal("Npgsql.EntityFrameworkCore.PostgreSQL", context.Database.ProviderName);
        Assert.Equal(ServiceEnvironmentFixture.DatabaseConnectionString, context.Database.GetConnectionString());
    }

    [Fact]
    public void DatabaseConnectionPort_ResolvesToEfCoreAdapter()
    {
        var connection = fixture.Factory.Services.GetRequiredService<IDatabaseConnection>();

        Assert.IsType<EfCoreDatabaseConnection>(connection);
    }

    [Fact]
    public async Task EfCoreAdapter_ReportsConnectivity_ForSwappableProvider()
    {
        var services = new ServiceCollection();
        services.AddDbContextFactory<AccountManagementDbContext>(options => options.UseInMemoryDatabase("accounts"));
        services.AddSingleton<IDatabaseConnection, EfCoreDatabaseConnection>();
        await using var provider = services.BuildServiceProvider();

        var canConnect = await provider.GetRequiredService<IDatabaseConnection>().CanConnectAsync();

        Assert.True(canConnect);
    }
}
