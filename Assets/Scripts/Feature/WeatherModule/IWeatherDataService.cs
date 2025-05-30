namespace ShootingCar.Feature.WeatherModule
{
    public interface IWeatherDataService
    {
        public WeatherData GetWeatherData(WeatherType weatherType);
    }
}
