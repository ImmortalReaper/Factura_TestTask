using Addressables;
using Core.PrefabFactory;
using UnityEngine;
using Zenject;

public class WeatherService : ITickable, IInitializable
{
    private readonly PlayerEntityModel _playerEntityModel;
    private readonly IPrefabFactory _prefabFactory;

    private GameObject _weatherObject;
    
    public WeatherService(IPrefabFactory prefabFactory,
                          PlayerEntityModel playerEntityModel)
    {
        _prefabFactory = prefabFactory;
        _playerEntityModel = playerEntityModel;
    }

    public void Initialize()
    {
        _weatherObject = _prefabFactory.Create(Address.Weather.SandStorm);
    }
    
    public void Tick()
    {
        _weatherObject.transform.position = _playerEntityModel.PlayerEntity.transform.position;
    }
}
