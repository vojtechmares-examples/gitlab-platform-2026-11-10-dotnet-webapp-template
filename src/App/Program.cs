var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.MapRazorPages();
app.MapHealthChecks("/health");

// Which build runs where: the first thing to look at after a deployment
app.MapGet("/api/info", (IWebHostEnvironment environment, IConfiguration configuration) => new
{
    application = environment.ApplicationName,
    build = configuration["APP_BUILD"] ?? "local",
    environment = environment.EnvironmentName,
    host = Environment.MachineName,
    runtime = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription,
    time = DateTimeOffset.UtcNow,
});

app.Run();

// Lets the integration tests reach the entry point (WebApplicationFactory<Program>)
public partial class Program;
