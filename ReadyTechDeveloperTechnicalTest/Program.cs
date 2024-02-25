using ReadyTechDeveloperTechnicalTest;
using Service;
using Service.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IWebApiRequest, WebApiRequest>();
builder.Services.AddScoped<ICoffeeMachineService, CoffeeMachineService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseEndpointCounterMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();

