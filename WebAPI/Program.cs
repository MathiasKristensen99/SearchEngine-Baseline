using DB;
using Logic;
using RestSharp;
using WebAPI.DB;
using WebAPI.Logic;


var restClient = new RestClient("http://load-balancer");

restClient.Post(new RestRequest("api/Configuration?url=http://" + Environment.MachineName, Method.Post));

Console.WriteLine("Hostname: " + Environment.MachineName);


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
