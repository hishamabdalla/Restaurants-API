using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService weatherForecast;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IWeatherForecastService weatherForecast)
        {
            _logger = logger;
            this.weatherForecast = weatherForecast;
        }

        [HttpPost(Name = "Generate")]
        public IActionResult Generate(int count,int minTemprature,int maxTemprature)
        {
          if(count <= 0 || maxTemprature<=minTemprature)
          {
                return BadRequest();
          }
          var result=weatherForecast.GetWeatherForecasts(count, minTemprature, maxTemprature);
          return Ok(result);
        }
    }
}
