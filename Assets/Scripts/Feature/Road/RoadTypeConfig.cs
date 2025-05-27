using Addressables;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RoadTypeConfig), menuName = "Configurations/RoadTypes/" + nameof(RoadTypeConfig))]
public class RoadTypeConfig : ScriptableObject
{
    public RoadTypes RoadType;
    public float RoadLength;
    public float SpawnThreshold = 20f;
    public int InitialBuffer = 5;

    public string GetAddressableName(RoadTypes roadType)
    {
        switch (roadType)
        {
            case RoadTypes.Desert:
                return Address.Prefabs.Ground;
            default:
                return Address.Prefabs.Ground;
        }
    }
    
    public enum RoadTypes
    {
        Desert
    }
}
