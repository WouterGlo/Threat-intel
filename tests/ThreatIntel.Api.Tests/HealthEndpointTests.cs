using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ThreatIntel.Api.Tests;

public class HealthEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_ReturnsOk_WithHealthyStatus()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains("healthy", body);
    }

    [Fact]
    public async Task UnknownRoute_ReturnsNotFound_WithoutLeakingDetails()
    {
        var response = await _client.GetAsync("/does-not-exist");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.DoesNotContain("Exception", body);
        Assert.DoesNotContain("StackTrace", body);
    }
}