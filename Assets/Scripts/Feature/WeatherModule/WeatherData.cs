using System;
using UnityEngine;

namespace ShootingCar.Feature.WeatherModule
{
    [Serializable]
    public class WeatherData 
    {
        public WeatherType WeatherType;
        public GameObject WeatherPrefab;
    }
}
