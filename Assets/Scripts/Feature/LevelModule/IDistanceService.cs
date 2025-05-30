using System;

namespace ShootingCar.Feature.LevelModule
{
    public interface IDistanceService
    {
        public float CompleteNormalizedDistance { get; }
        public float CompleteDistance { get; }
        public event Action OnLevelCompleted;
        public void StartTracking(LevelConfig levelConfig);
        public void StopTracking();
    }
}
