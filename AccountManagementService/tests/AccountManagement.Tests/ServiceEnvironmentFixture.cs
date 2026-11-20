using AccountManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AccountManagement.Tests;

/// <summary>
/// Supplies configuration only through environment variables, pointing at unreachable
/// dependencies so tests prove the service starts without DB, Redis or Elasticsearch.
/// </summary>
public sealed class ServiceEnvironmentFixture : IDisposable
{
    public const string DatabaseConnectionString = "Host=127.0.0.1;Port=1;Database=accounts;Username=test;Password=test";

    private static readonly Dictionary<string, string> _variables = new()
    {
        [ServiceSettings.HttpPortKey] = "5080",
        [ServiceSettings.DatabaseConnectionStringKey] = DatabaseConnectionString,
        [ServiceSettings.RedisConnectionStringKey] = "127.0.0.1:1",
        [ServiceSettings.ElasticsearchUrlKey] = "http://127.0.0.1:1",
    };

    public ServiceEnvironmentFixture()
    {
        foreach (var (key, value) in _variables)
        {
            Environment.SetEnvironmentVariable(key, value);
        }

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseEnvironment("Development"));
    }

    public WebApplicationFactory<Program> Factory { get; }

    public void Dispose()
    {
        Factory.Dispose();
        foreach (var key in _variables.Keys)
        {
            Environment.SetEnvironmentVariable(key, null);
        }
    }
}

[CollectionDefinition(Name)]
public sealed class ServiceEnvironmentGroup : ICollectionFixture<ServiceEnvironmentFixture>
{
    public const string Name = "Service environment";
}
