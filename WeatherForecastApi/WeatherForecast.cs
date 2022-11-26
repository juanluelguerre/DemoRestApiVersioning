namespace WeatherForecastApi;

/// <summary>
/// Weather forecast detail
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// The date
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Temperature en Celsius
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Temperature in Fahrenheit
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Forecast summary
    /// </summary>
    public string? Summary { get; set; }
}
