using AccountManagement.Infrastructure;
using AccountManagement.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Sources.Clear();
builder.Configuration.AddEnvironmentVariables();

var settings = ServiceSettings.Load(builder.Configuration);

builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(settings.HttpPort));

builder.Services.AddInfrastructure(settings);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }))
    .WithName("Health")
    .WithTags("Health");

app.Run();

public partial class Program;
