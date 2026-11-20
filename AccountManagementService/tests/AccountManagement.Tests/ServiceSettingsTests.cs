using AccountManagement.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace AccountManagement.Tests;

public class ServiceSettingsTests
{
    private static Dictionary<string, string?> ValidValues() => new()
    {
        [ServiceSettings.HttpPortKey] = "8080",
        [ServiceSettings.DatabaseConnectionStringKey] = "Host=db.test;Database=accounts;Username=u;Password=s3cr3t",
        [ServiceSettings.RedisConnectionStringKey] = "redis.test:6379,password=r3d1s",
        [ServiceSettings.ElasticsearchUrlKey] = "http://search.test:9200",
    };

    private static IConfiguration Build(IDictionary<string, string?> values) =>
        new ConfigurationBuilder().AddInMemoryCollection(values).Build();

    [Fact]
    public void Load_WithAllValues_ReturnsSettings()
    {
        var settings = ServiceSettings.Load(Build(ValidValues()));

        Assert.Equal(8080, settings.HttpPort);
        Assert.Equal("Host=db.test;Database=accounts;Username=u;Password=s3cr3t", settings.DatabaseConnectionString);
        Assert.Equal("redis.test:6379,password=r3d1s", settings.RedisConnectionString);
        Assert.Equal(new Uri("http://search.test:9200"), settings.ElasticsearchUri);
    }

    [Theory]
    [InlineData(ServiceSettings.HttpPortKey)]
    [InlineData(ServiceSettings.DatabaseConnectionStringKey)]
    [InlineData(ServiceSettings.RedisConnectionStringKey)]
    [InlineData(ServiceSettings.ElasticsearchUrlKey)]
    public void Load_WithMissingValue_FailsFastNamingTheVariable(string key)
    {
        var values = ValidValues();
        values.Remove(key);

        var ex = Assert.Throws<InvalidOperationException>(() => ServiceSettings.Load(Build(values)));

        Assert.Contains(key, ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Load_WithBlankValue_IsTreatedAsMissing()
    {
        var values = ValidValues();
        values[ServiceSettings.DatabaseConnectionStringKey] = "   ";

        var ex = Assert.Throws<InvalidOperationException>(() => ServiceSettings.Load(Build(values)));

        Assert.Contains(ServiceSettings.DatabaseConnectionStringKey, ex.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("65536")]
    [InlineData("-1")]
    [InlineData("http")]
    public void Load_WithInvalidPort_Fails(string port)
    {
        var values = ValidValues();
        values[ServiceSettings.HttpPortKey] = port;

        var ex = Assert.Throws<InvalidOperationException>(() => ServiceSettings.Load(Build(values)));

        Assert.Contains(ServiceSettings.HttpPortKey, ex.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("not a url")]
    [InlineData("ftp://search.test")]
    public void Load_WithInvalidElasticsearchUrl_Fails(string url)
    {
        var values = ValidValues();
        values[ServiceSettings.ElasticsearchUrlKey] = url;

        var ex = Assert.Throws<InvalidOperationException>(() => ServiceSettings.Load(Build(values)));

        Assert.Contains(ServiceSettings.ElasticsearchUrlKey, ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Load_ErrorMessages_DoNotContainSecretValues()
    {
        var values = ValidValues();
        values.Remove(ServiceSettings.ElasticsearchUrlKey);
        values[ServiceSettings.HttpPortKey] = "s3cr3t";

        var missing = Assert.Throws<InvalidOperationException>(() => ServiceSettings.Load(Build(values)));
        values[ServiceSettings.ElasticsearchUrlKey] = "http://search.test:9200";
        var invalid = Assert.Throws<InvalidOperationException>(() => ServiceSettings.Load(Build(values)));

        Assert.DoesNotContain("s3cr3t", missing.Message, StringComparison.Ordinal);
        Assert.DoesNotContain("r3d1s", missing.Message, StringComparison.Ordinal);
        Assert.DoesNotContain("s3cr3t", invalid.Message, StringComparison.Ordinal);
    }
}
