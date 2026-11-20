using AccountManagement.Application.Ports;
using AccountManagement.Infrastructure.Configuration;
using AccountManagement.Infrastructure.Persistence;
using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AccountManagement.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure adapters. Clients connect lazily, so the service can start
    /// without the database, Redis or Elasticsearch being reachable.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ServiceSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        services.AddSingleton(settings);

        services.AddDbContextFactory<AccountManagementDbContext>(options =>
            options.UseNpgsql(settings.DatabaseConnectionString));
        services.AddSingleton<IDatabaseConnection, EfCoreDatabaseConnection>();

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var options = ConfigurationOptions.Parse(settings.RedisConnectionString);
            options.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(options);
        });

        services.AddSingleton(_ => new ElasticsearchClient(new ElasticsearchClientSettings(settings.ElasticsearchUri)));

        return services;
    }
}
