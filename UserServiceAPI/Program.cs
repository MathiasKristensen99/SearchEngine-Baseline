using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Diagnostics;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using UserServiceAPI.Repository;
using static System.Net.WebRequestMethods;

// Configure Tracing
// Extensions: OpenTelemetry, OpenTelemetry.Exporter.Console, OpenTelemetry.Exporter.Zipkin
Console.WriteLine("ServiceName = " + Assembly.GetExecutingAssembly().GetName().Name);
using var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddZipkinExporter(config =>
    {
        config.Endpoint = new Uri("http://localhost:9411");
    })
    .AddConsoleExporter()
    .AddSource(DiagnosticsConfig.ActivitySource.Name)
    .SetResourceBuilder(
        ResourceBuilder
            .CreateDefault()
            .AddService(DiagnosticsConfig.ServiceName,DiagnosticsConfig.ActivitySource.Version)
    )
    .Build();

//Configure Logging
//Extensions: Serilog, Serilog.Enrichers.Span, Serilog.Sinks.Console, Serilog.Sinks.Seq
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.WithSpan()
    .WriteTo.Seq("http://seq")
    .WriteTo.Console()
    .CreateLogger();


// REST CLIENT
//var restClient = new RestClient("http://load-balancer");
//restClient.Post(new RestRequest("api/Configuration?url=http://" + Environment.MachineName, Method.Post));
//Console.WriteLine("Hostname: " + Environment.MachineName);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlite("Data Source=/data/userDatabase.db"));
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCors(options => options
    .AddPolicy("dev-policy", policyBuilder =>
        policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    ctx.Database.EnsureCreatedAsync();
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("dev-policy");

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class DiagnosticsConfig
{
    // Monitoring and Tracing
    public static readonly string ServiceName = Assembly.GetExecutingAssembly().GetName().Name;
    private const string Version = "1.0.0";
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName, Version);
};