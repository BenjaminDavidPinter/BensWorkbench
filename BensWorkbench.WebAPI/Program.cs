using BensWorkbench.Models;
using BensWorkbench.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", 
    "Bracing", 
    "Chilly", 
    "Cool", 
    "Mild", 
    "Warm", 
    "Balmy", 
    "Hot", 
    "Sweltering", 
    "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = new WeatherService();
    var thisForecast = forecast.GetWeather(40.245663, -74.846001);
    return thisForecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
