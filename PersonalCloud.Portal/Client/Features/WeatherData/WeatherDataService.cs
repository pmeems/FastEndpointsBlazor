#nullable disable

namespace PersonalCloud.Portal.Client.Features.WeatherData
{
    public class WeatherDataService
    {
        private readonly HttpClient _http;
        private readonly ILogger<WeatherDataService> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        public WeatherDataService(HttpClient http, ILogger<WeatherDataService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<WeatherForecast>> GetAll()
        {
            _logger.LogDebug("In GetAll");
            try
            {
                // Get data, all at once:
                var response = await _http.GetAsync("weatherforecast/get");
                response.EnsureSuccessStatusCode();
                return await JsonSerializer.DeserializeAsync<List<WeatherForecast>>(await response.Content.ReadAsStreamAsync(),
                    _jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async IAsyncEnumerable<WeatherForecast> GetAllStream()
        {
            _logger.LogDebug("In GetAllStream");

            // https://www.youtube.com/watch?v=BbtMpt_8RGk
            // This is the pattern I'm using for retrieving an IAsyncEnumerable
            // from an API endpoint and returning an IAsyncEnumerable, one record
            // at a time, using yield return 

            // This is just a System.IO.Stream
            Stream stream = await _http.GetStreamAsync("weatherforecast/getstream");

            if (stream != null)
            {
                using (stream!)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<WeatherForecast>(stream, options))
                    {
                        yield return item;
                    }

                }
            }
        }
    }
}
