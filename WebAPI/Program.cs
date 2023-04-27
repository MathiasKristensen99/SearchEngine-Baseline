using System.Diagnostics;
using System.Reflection;
using DB;
using Logic;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RestSharp;
using Serilog;
using Serilog.Enrichers.Span;
using WebAPI.DB;
using WebAPI.Logic;


// Configure Tracing
// Extensions: OpenTelemetry, OpenTelemetry.Exporter.Console, OpenTelemetry.Exporter.Zipkin
Console.WriteLine("ServiceName = " + Assembly.GetExecutingAssembly().GetName().Name);
using var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddZipkinExporter()
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
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.Console()
    .CreateLogger();

// Rest Client
var restClient = new RestClient("http://load-balancer");
restClient.Post(new RestRequest("api/Configuration?url=http://" + Environment.MachineName, Method.Post));

Console.WriteLine("Hostname: " + Environment.MachineName);
Console.WriteLine("BUILD VERSION: 14");


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISearchLogic, SearchLogic>();
builder.Services.AddSingleton<IDatabase, Database>();

builder.Services.AddCors(options => options
    .AddPolicy("dev-policy", policyBuilder =>
        policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

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