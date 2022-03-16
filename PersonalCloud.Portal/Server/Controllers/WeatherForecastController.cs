using Microsoft.AspNetCore.Mvc;

namespace PersonalCloud.Portal.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogDebug("WeatherForecastController.Get");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("getstream")]
        public async IAsyncEnumerable<WeatherForecast> GetAllStream()
        {
            _logger.LogDebug("WeatherForecastController.GetAllStream");
            await Task.Delay(0);
            var data = Enumerable.Range(1, 5_000).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
            foreach (var item in data)
            {
                yield return item;
            }
        }
    }
}