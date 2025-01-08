
namespace Restaurants.API
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> GetWeatherForecasts(int count, int minTempreture, int maxTemprature)
        {
            var weathers = Enumerable.Range(1, count).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(minTempreture, maxTemprature),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
             .ToArray();

            return weathers;
        }
    }
}
