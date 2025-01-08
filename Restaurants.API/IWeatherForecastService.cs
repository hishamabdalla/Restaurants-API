namespace Restaurants.API
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts(int count,int minTempreture,int maxTemprature);
    }
}
