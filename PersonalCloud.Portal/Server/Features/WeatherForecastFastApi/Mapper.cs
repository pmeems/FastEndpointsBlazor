namespace WeatherForecastFastApi;

public class Mapper : Mapper<Request, Response, WeatherForecast>
{
    public override Response FromEntity(WeatherForecast e)
    {
        return new Response
        {
            Date = e.Date,
            TemperatureC = e.TemperatureC,
            TemperatureF = e.TemperatureF,
            Summary = e.Summary
        };
    }
}
