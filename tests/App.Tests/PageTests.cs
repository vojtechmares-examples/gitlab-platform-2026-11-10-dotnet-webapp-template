using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace App.Tests;

public class PageTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task The_home_page_renders_inside_the_layout()
    {
        var response = await _client.GetAsync("/");
        var html = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("<h1>Welcome</h1>", html);
        Assert.Contains("<header>App</header>", html);
    }

    [Fact]
    public async Task Health_is_ok()
    {
        Assert.Equal(HttpStatusCode.OK, (await _client.GetAsync("/health")).StatusCode);
    }

    [Fact]
    public async Task Info_reports_the_build()
    {
        var info = await _client.GetFromJsonAsync<Dictionary<string, object>>("/api/info");

        Assert.Equal("local", info!["build"].ToString());
    }

    [Fact]
    public async Task An_unknown_page_is_404()
    {
        Assert.Equal(HttpStatusCode.NotFound, (await _client.GetAsync("/nope")).StatusCode);
    }
}
