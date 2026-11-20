using System.Globalization;
using Microsoft.Extensions.Configuration;
using Npgsql;
using StackExchange.Redis;

namespace AccountManagement.Infrastructure.Configuration;

/// <summary>
/// Service configuration, loaded exclusively from environment variables.
/// </summary>
public sealed record ServiceSettings(
    int HttpPort,
    string DatabaseConnectionString,
    string RedisConnectionString,
    Uri ElasticsearchUri)
{
    public const string HttpPortKey = "HTTP_PORT";
    public const string DatabaseConnectionStringKey = "DATABASE_CONNECTION_STRING";
    public const string RedisConnectionStringKey = "REDIS_CONNECTION_STRING";
    public const string ElasticsearchUrlKey = "ELASTICSEARCH_URL";

    public static IReadOnlyList<string> RequiredKeys { get; } =
    [
        HttpPortKey,
        DatabaseConnectionStringKey,
        RedisConnectionStringKey,
        ElasticsearchUrlKey,
    ];

    /// <summary>
    /// Builds settings from configuration, failing fast when any required value is missing or invalid.
    /// Error messages name the offending variables only and never include their values.
    /// </summary>
    public static ServiceSettings Load(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var missing = RequiredKeys.Where(key => string.IsNullOrWhiteSpace(configuration[key])).ToList();
        if (missing.Count > 0)
        {
            throw new InvalidOperationException(
                $"Missing required environment variable(s): {string.Join(", ", missing)}.");
        }

        var errors = new List<string>();

        if (!int.TryParse(configuration[HttpPortKey], NumberStyles.None, CultureInfo.InvariantCulture, out var port)
            || port is < 1 or > 65535)
        {
            errors.Add($"{HttpPortKey} must be an integer between 1 and 65535.");
        }

        if (!Uri.TryCreate(configuration[ElasticsearchUrlKey], UriKind.Absolute, out var elasticsearchUri)
            || (elasticsearchUri.Scheme != Uri.UriSchemeHttp && elasticsearchUri.Scheme != Uri.UriSchemeHttps))
        {
            errors.Add($"{ElasticsearchUrlKey} must be an absolute http(s) URL.");
        }

        if (!IsValidDatabaseConnectionString(configuration[DatabaseConnectionStringKey]!))
        {
            errors.Add($"{DatabaseConnectionStringKey} must be a valid PostgreSQL (Npgsql) connection string with a Host.");
        }

        if (!IsValidRedisConnectionString(configuration[RedisConnectionStringKey]!))
        {
            errors.Add($"{RedisConnectionStringKey} must be a valid StackExchange.Redis configuration string with an endpoint.");
        }

        if (errors.Count > 0)
        {
            throw new InvalidOperationException($"Invalid environment configuration: {string.Join(" ", errors)}");
        }

        return new ServiceSettings(
            port,
            configuration[DatabaseConnectionStringKey]!,
            configuration[RedisConnectionStringKey]!,
            elasticsearchUri!);
    }

    private static bool IsValidDatabaseConnectionString(string value)
    {
        try
        {
            return !string.IsNullOrWhiteSpace(new NpgsqlConnectionStringBuilder(value).Host);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    private static bool IsValidRedisConnectionString(string value)
    {
        try
        {
            return ConfigurationOptions.Parse(value).EndPoints.Count > 0;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
