namespace ShootingCar.Feature.LevelModule
{
    public interface ILevelConfigService
    {
        LevelConfig GetLevelConfig(int levelIndex);
        int LevelCount { get; }
    }
}
