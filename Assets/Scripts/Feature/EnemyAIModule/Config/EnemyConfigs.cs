using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootingCar.Feature.EnemyAIModule.Config
{
    [CreateAssetMenu(fileName = nameof(EnemyConfigs), menuName = "Configurations/Enemy Config/" + nameof(EnemyConfigs))]
    public class EnemyConfigs : ScriptableObject
    {
        public List<EnemyData> EnemyConfig;
        public EnemyData DefaultEnemyConfig;
    
        public EnemyData GetEnemyConfig(EnemyType enemyType) => 
            EnemyConfig.FirstOrDefault(enemyData => enemyData.EnemyType == enemyType) ?? DefaultEnemyConfig;
    }
}
