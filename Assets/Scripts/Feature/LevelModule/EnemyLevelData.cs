using System;
using ShootingCar.Feature.EnemyAIModule.Config;
using UnityEngine;

namespace ShootingCar.Feature.LevelModule
{
    [Serializable]
    public class EnemyLevelData
    {
        public EnemyType EnemyType;
        public AnimationCurve SpawnCurve;
    
        public int EvaluateSpawnCount(float currentDistance, float levelFinishDistance)
        {
            float clamped = Mathf.Clamp(currentDistance, 0f, levelFinishDistance);
            float value = SpawnCurve.Evaluate(clamped);
            return Mathf.RoundToInt(value);
        }
    }
}
