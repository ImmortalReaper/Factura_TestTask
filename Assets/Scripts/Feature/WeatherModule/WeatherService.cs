using Core.PrefabFactory;
using UnityEngine;
using Zenject;

public class WeatherService : ITickable, IInitializable
{
    private readonly PlayerEntityModel _playerEntityModel;
    private readonly IPrefabFactory _prefabFactory;
    private readonly IWeatherDataService _weatherDataService;

    private WeatherData _weatherData;
    private GameObject _weatherObject;
    
    public WeatherService(IPrefabFactory prefabFactory,
                          PlayerEntityModel playerEntityModel,
                          IWeatherDataService weatherDataService)
    {
        _prefabFactory = prefabFactory;
        _playerEntityModel = playerEntityModel;
        _weatherDataService = weatherDataService;
    }

    public void Initialize()
    {
        _weatherData = _weatherDataService.GetWeatherData(WeatherType.SandStorm);
        _weatherObject = _prefabFactory.Create(_weatherData.WeatherPrefab);
    }
    
    public void Tick()
    {
        _weatherObject.transform.position = _playerEntityModel.PlayerEntity.transform.position;
    }
}
