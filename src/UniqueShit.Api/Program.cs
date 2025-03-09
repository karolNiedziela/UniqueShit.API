using UniqueShit.Api.Endpoints;
using UniqueShit.Application;
using UniqueShit.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
#pragma warning disable CA5394 // Do not use insecure randomness
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
#pragma warning restore CA5394 // Do not use insecure randomness
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.AddOfferEndpoints();
app.AddOfferFiltersEndpoints();

await app.RunAsync();

#pragma warning disable S3903 // Types should be defined in named namespaces
internal sealed record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
#pragma warning restore S3903 // Types should be defined in named namespaces
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
