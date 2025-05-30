using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ShootingCar.Feature.CarModule;
using ShootingCar.Feature.PlayerData;
using UnityEngine;
using Zenject;

namespace ShootingCar.Feature.LevelModule
{
    public class DistanceService : IDistanceService, IInitializable, IDisposable
    {
        private readonly PlayerEntityModel _playerModel;
    
        private LevelConfig _levelConfig;
        private Transform _carTransform;
        private bool _levelCompleted;
        private CancellationTokenSource _cts;
    
        public float CompleteNormalizedDistance => Mathf.Abs(_carTransform.transform.position.z) / _levelConfig.LevelFinishDistance;
        public float CompleteDistance => Mathf.Abs(_carTransform.transform.position.z);
    
        public event Action OnLevelCompleted;
    
        public DistanceService(PlayerEntityModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            _carTransform = _playerModel.PlayerEntity.GetComponent<CarController>().CarTransform;
            _levelCompleted = false;
        }
    
        public void StartTracking(LevelConfig levelConfig)
        {
            _levelCompleted = false;
            _levelConfig = levelConfig;
            StopTracking();
            _cts = new CancellationTokenSource();
            TrackDistanceAsync(_cts.Token).Forget();
        }
    
        public void StopTracking()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts?.Cancel();
                _cts?.Dispose();
            }
        }

        private async UniTaskVoid TrackDistanceAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && !_levelCompleted)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);

                if (!_levelCompleted && CompleteDistance >= _levelConfig.LevelFinishDistance)
                {
                    _levelCompleted = true;
                    OnLevelCompleted?.Invoke();
                    StopTracking();
                }
            }
        }

        public void Dispose()
        {
            StopTracking();
        }
    }
}
