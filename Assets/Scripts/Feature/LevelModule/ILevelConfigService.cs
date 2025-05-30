public interface ILevelConfigService
{
    LevelConfig GetLevelConfig(int levelIndex);
    int LevelCount { get; }
}
