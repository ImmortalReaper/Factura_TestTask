namespace ShootingCar.Feature.LevelModule
{
    public interface IEnemySpawnService
    {
        public void StopSpawning();
        public void StartSpawning(LevelConfig levelConfig);
    }
}
