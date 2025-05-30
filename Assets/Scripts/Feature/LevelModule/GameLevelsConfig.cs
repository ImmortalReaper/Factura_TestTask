using System.Collections.Generic;
using UnityEngine;

namespace ShootingCar.Feature.LevelModule
{
    [CreateAssetMenu(fileName = nameof(GameLevelsConfig), menuName = "Configurations/Game Levels Config/" + nameof(GameLevelsConfig))]
    public class GameLevelsConfig : ScriptableObject
    {
        public List<LevelConfig> levelConfigs;
    }
}
