namespace WeatherForecastFastApi;

public class GetEndpoint : Endpoint<Request, ListResponse, Mapper>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<GetEndpoint> _logger;

    public GetEndpoint(ILogger<GetEndpoint> logger)
    {
        _logger = logger;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/weather/{days}");
        ResponseCache(1);
        AllowAnonymous();

        Describe(x => x
            .Accepts<Request>("application/json")
            .Produces<ListResponse>(200, "application/json")
            );
    }

    public override async Task HandleAsync(Request req, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Retrieving weather data for {Days} days.", req.Days);
        var forecasts = Enumerable.Range(1, req.Days).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        var response = new ListResponse
        {
            Forecasts = forecasts.Select(Map.FromEntity)
        };

        await SendAsync(response, cancellation: cancellationToken);
    }
}
