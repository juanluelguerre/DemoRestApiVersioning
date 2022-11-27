using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WeatherForecastApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddEndpointsApiExplorer()
    .AddCustomApiVersioning();

var app = builder.Build();
var apiVersionDescriptionProvider =
    app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        // Allows multiple versions of our routes.
        // .Reverse(), first shown most recent version.
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
        {
            config.SwaggerEndpoint(
                url: $"/swagger/{description.GroupName}/swagger.json",
                name: $"Weather Forecast API - {description.GroupName.ToUpper()}");
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
