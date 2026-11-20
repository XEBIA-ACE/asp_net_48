using System.Net;
using System.Text.Json;

namespace AccountManagement.Tests;

/// <summary>
/// AC-1: the service starts with environment-only configuration and its health-check responds.
/// </summary>
[Collection(ServiceEnvironmentGroup.Name)]
public class HealthEndpointTests(ServiceEnvironmentFixture fixture)
{
    [Fact]
    public async Task Health_ReturnsOk_WithoutExternalDependencies()
    {
        using var client = fixture.Factory.CreateClient();

        using var response = await client.GetAsync(new Uri("/health", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("ok", body.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task OpenApiDocument_DescribesHealthEndpoint()
    {
        using var client = fixture.Factory.CreateClient();

        using var response = await client.GetAsync(new Uri("/swagger/v1/swagger.json", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.True(body.RootElement.GetProperty("paths").TryGetProperty("/health", out _));
    }
}
