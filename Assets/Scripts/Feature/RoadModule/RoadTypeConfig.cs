using UnityEngine;

[CreateAssetMenu(fileName = nameof(RoadTypeConfig), menuName = "Configurations/RoadTypes/" + nameof(RoadTypeConfig))]
public class RoadTypeConfig : ScriptableObject
{
    public GameObject RoadTypePrefab;
    public float RoadLength;
    public float SpawnThreshold = 20f;
    public int InitialBuffer = 5;
}
