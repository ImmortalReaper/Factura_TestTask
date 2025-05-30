using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(WeatherConfigs), menuName = "Configurations/Weather Config/" + nameof(WeatherConfigs))]
public class WeatherConfigs : ScriptableObject
{
    public List<WeatherData> WeatherConfig;
    public WeatherData DeafultConfig;
    
    public WeatherData GetWeatherData(WeatherType weatherType) => 
        WeatherConfig.FirstOrDefault(weatherData => weatherData.WeatherType == weatherType) ?? DeafultConfig;
}
