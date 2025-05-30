using System;
using System.Collections.Generic;

namespace ShootingCar.Feature.LevelModule
{
    public class LevelConfigService : ILevelConfigService
    {
        private readonly List<LevelConfig> _configs;
    
        public int LevelCount => _configs.Count;
    
        public LevelConfigService(GameLevelsConfig gameLevelsConfig)
        {
            _configs = gameLevelsConfig.levelConfigs;
        }

        public LevelConfig GetLevelConfig(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= _configs.Count)
                throw new ArgumentOutOfRangeException(nameof(levelIndex));
            return _configs[levelIndex];
        }
    }
}
