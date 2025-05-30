using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ShootingCar.Core.ObjectPool;
using ShootingCar.Feature.EnemyAIModule;
using ShootingCar.Feature.EnemyAIModule.Config;
using ShootingCar.Feature.PlayerData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootingCar.Feature.LevelModule
{
    public class EnemySpawnService : IEnemySpawnService, IDisposable
    {
        private IObjectPoolService _objectPoolService;
        private PlayerEntityModel _playerEntityModel;
        private IEnemyDataService _enemyDataService;
        private ILevelConfigService _levelConfigService;
        private GameLevelsConfig _gameLevelsConfig;
    
        private Dictionary<EnemyType, int> _lastSpawnCounts = new();
        private CancellationTokenSource _cancellationTokenSource;
        private LevelConfig  _levelConfig;
    
        public EnemySpawnService(IObjectPoolService objectPoolService, 
            GameLevelsConfig gameLevelsConfig, 
            PlayerEntityModel playerEntityModel, 
            IEnemyDataService enemyDataService)
        {
            _playerEntityModel = playerEntityModel;
            _enemyDataService = enemyDataService;
            _objectPoolService = objectPoolService;
            _gameLevelsConfig = gameLevelsConfig;
        }

        private async UniTask SpawnEnemies(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                float dist = Mathf.Abs(_playerEntityModel.PlayerEntity.transform.position.z);
                Vector3 playerPos = _playerEntityModel.PlayerEntity.transform.position;
                Vector3 spawnPos = playerPos - _playerEntityModel.PlayerEntity.transform.forward * _levelConfig.EnemySpawnOffset;
            
                if (spawnPos.z > _levelConfig.LevelFinishDistance) 
                    return;
            
                foreach (EnemyLevelData data in _levelConfig.enemySpawnConfigs)
                {
                    EnemyType type = data.EnemyType;
                    int desired = data.EvaluateSpawnCount(dist, _levelConfig.LevelFinishDistance);
                    int last = _lastSpawnCounts[type];

                    if (desired <= last)
                        continue;

                    int toSpawn = desired - last;
                    _lastSpawnCounts[type] = desired;

                    SpawnEnemies(type, toSpawn, spawnPos);
                }
            
                await UniTask.Yield(cancellationToken: cancellationToken);
            }
        }
    
        private void SpawnEnemies(EnemyType type, int count, Vector3 spawnPos)
        {
            EnemyData enemyData = _enemyDataService.GetEnemyData(type);
            for (int i = 0; i < count; i++)
            {
                float targetX = Random.Range(spawnPos.x - _levelConfig.EnemySpawnRadius, spawnPos.x + _levelConfig.EnemySpawnRadius);
                GameObject enemy = _objectPoolService.GetTimed(enemyData.EnemyPrefab, 30f);
                enemy.GetComponent<IEnemy>().SetDefaultState();
                enemy.transform.position = new Vector3(spawnPos.x + targetX, spawnPos.y, spawnPos.z);
            }
        }

        private void InitializeLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
            _lastSpawnCounts.Clear();
            foreach (var cfg in _levelConfig.enemySpawnConfigs)
                _lastSpawnCounts[cfg.EnemyType] = 0;
        }
    
        public void StopSpawning()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    
        public void StartSpawning(LevelConfig levelConfig)
        {
            InitializeLevelConfig(levelConfig);
            StopSpawning();
            _cancellationTokenSource = new CancellationTokenSource();
            SpawnEnemies(_cancellationTokenSource.Token).Forget();
        }

        public void Dispose()
        {
            StopSpawning();
        }
    }
}
