using System.Collections.Generic;
using System.Linq;
using ShootingCar.AddressablesAddress;
using ShootingCar.Core.ObjectPool;
using ShootingCar.Feature.PlayerData;
using UnityEngine;
using Zenject;

namespace ShootingCar.Feature.RoadModule
{
    public class RoadGeneratorService : IInitializable, ITickable
    {
        private readonly IObjectPoolService _objectPoolService;
        private readonly PlayerEntityModel _playerEntityModel;
        private readonly RoadTypeConfig _roadTypeConfig;
    
        List<GameObject> activeTiles = new();

        public RoadGeneratorService(IObjectPoolService objectPoolService, 
            PlayerEntityModel playerEntityModel, 
            [Inject(Id = Address.Configs.DesertRoadTypeConfig)] RoadTypeConfig roadTypeConfig)
        {
            _objectPoolService = objectPoolService;
            _playerEntityModel = playerEntityModel;
            _roadTypeConfig = roadTypeConfig;
        }

        public void Initialize()
        {
            Vector3 spawnPos = Vector3.zero;
            for (int i = 0; i < _roadTypeConfig.InitialBuffer; i++)
            {
                var tile = _objectPoolService.Get(_roadTypeConfig.RoadTypePrefab);
                tile.transform.position = spawnPos;
                activeTiles.Add(tile);
                spawnPos = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z - _roadTypeConfig.RoadLength);
            }
        }

        public void Tick()
        {
            if(activeTiles.Count == 0) return;
        
            var lastTile = activeTiles.Last();
            float distToEnd = Vector3.Distance(_playerEntityModel.PlayerEntity.transform.position, lastTile.transform.position);
            if (distToEnd < _roadTypeConfig.SpawnThreshold)
                SpawnNext();
        
            var first = activeTiles.First();
            float distFromStart = Vector3.Distance(_playerEntityModel.PlayerEntity.transform.position, first.transform.position);
            if (distFromStart > _roadTypeConfig.RoadLength * 2)
                DespawnFirst();
        }

        private void SpawnNext()
        {
            var prev = activeTiles.Last();
            var tile = _objectPoolService.Get(_roadTypeConfig.RoadTypePrefab);
            tile.transform.position = new Vector3(prev.transform.position.x, prev.transform.position.y, prev.transform.position.z - _roadTypeConfig.RoadLength);
            activeTiles.Add(tile);
        }

        private void DespawnFirst()
        {
            var old = activeTiles.First();
            activeTiles.RemoveAt(0);
            _objectPoolService.Release(old);
        }
    }
}
