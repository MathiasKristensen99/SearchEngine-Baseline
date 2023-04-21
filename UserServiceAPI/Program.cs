using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using UserServiceAPI.Repository;
using static System.Net.WebRequestMethods;

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
