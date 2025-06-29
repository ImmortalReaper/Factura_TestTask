using System.Collections.Generic;
using ShootingCar.Feature.WeatherModule;
using UnityEngine;

namespace ShootingCar.Feature.LevelModule
{
    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "Configurations/Level Config/" + nameof(LevelConfig))]
    public class LevelConfig : ScriptableObject
    {
        public WeatherType WeatherType;
        public float LevelFinishDistance;
        public float EnemySpawnOffset = 60;
        public float EnemySpawnRadius = 4;
        public List<EnemyLevelData> enemySpawnConfigs;
    }
}
