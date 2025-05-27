using UnityEngine;

[CreateAssetMenu(fileName = nameof(CarStatsConfig), menuName = "Configurations/Car/" + nameof(CarStatsConfig))]
public class CarStatsConfig : ScriptableObject
{
    public float CarMaxSpeed;
    public float CarAcceleration;
    public float CarDeceleration;
    public float CarMaxHealth;
}
