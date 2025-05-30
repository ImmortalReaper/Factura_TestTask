using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameLevelsConfig), menuName = "Configurations/Game Levels Config/" + nameof(GameLevelsConfig))]
public class GameLevelsConfig : ScriptableObject
{
    public List<LevelConfig> levelConfigs;
}
