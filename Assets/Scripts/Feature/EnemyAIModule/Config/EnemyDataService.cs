public class EnemyDataService : IEnemyDataService
{
    private EnemyConfigs _enemyConfigs;

    public EnemyDataService(EnemyConfigs enemyConfigs)
    {
        _enemyConfigs = enemyConfigs;
    }

    public EnemyData GetEnemyData(EnemyType enemyType) => _enemyConfigs.GetEnemyConfig(enemyType);
}
