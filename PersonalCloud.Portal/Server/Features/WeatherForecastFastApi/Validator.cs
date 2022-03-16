namespace WeatherForecastFastApi;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Days)
            .GreaterThanOrEqualTo(1).WithMessage("Weather forecast days must be at least 1")
            .LessThanOrEqualTo(14).WithMessage("Weather forecast can't be retrieved past 14 days");
    }
}
