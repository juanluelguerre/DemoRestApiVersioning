using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastApi.Controllers;

/// <summary>
/// Endpoints used for managing Weather Forecasts
/// </summary>
// [ApiVersion("2.0")]
// [ApiVersion("1.1")]
// [ApiVersion("1.1-RC1")]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
        "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get the Weather forecast 
    /// </summary>
    /// <returns>The forecast for next 5 days</returns>
    /// <response code="200">Returns the forecast for next 5 days</response>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }
}
