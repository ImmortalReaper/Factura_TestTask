using Addressables;
using Core.PrefabFactory;
using UnityEngine;
using Zenject;

public class PlayerCarSpawner : MonoBehaviour
{
    private IPrefabFactory _prefabFactory;
    private PlayerEntityModel _playerEntityModel;
    
    [Inject]
    public void InjectDependencies(PlayerEntityModel playerEntityModel, IPrefabFactory prefabFactory)
    {
        _prefabFactory = prefabFactory;
        _playerEntityModel = playerEntityModel;
    }

    private void Start()
    {
        GameObject player = _prefabFactory.Create(Address.Prefabs.Car, transform.position);
        _playerEntityModel.PlayerEntity = player;
    }
}
