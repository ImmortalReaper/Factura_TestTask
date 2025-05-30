namespace ShootingCar.Feature.WeatherModule
{
    public class WeatherDataService : IWeatherDataService
    {
        private WeatherConfigs _weatherConfigs;

        public WeatherDataService(WeatherConfigs weatherConfigs)
        {
            _weatherConfigs = weatherConfigs;
        }

        public WeatherData GetWeatherData(WeatherType weatherType) => _weatherConfigs.GetWeatherData(weatherType);
    }
}
