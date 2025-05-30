using ShootingCar.AddressablesAddress;
using ShootingCar.Core.PrefabFactory;
using ShootingCar.Feature.PlayerData;
using UnityEngine;
using Zenject;

namespace ShootingCar.Feature.CarModule
{
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

        private void Awake()
        {
            GameObject player = _prefabFactory.Create(Address.Prefabs.Car, transform.position);
            _playerEntityModel.PlayerEntity = player;
        }
    }
}
