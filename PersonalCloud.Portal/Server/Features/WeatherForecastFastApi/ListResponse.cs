namespace WeatherForecastFastApi;

# nullable disable

public class ListResponse
{
    public IEnumerable<Response> Forecasts { get; set; }
}
