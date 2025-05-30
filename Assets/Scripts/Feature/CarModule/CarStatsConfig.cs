using UnityEngine;

[CreateAssetMenu(fileName = nameof(CarStatsConfig), menuName = "Configurations/Car/" + nameof(CarStatsConfig))]
public class CarStatsConfig : ScriptableObject
{
    public float CarForwardMaxSpeed;
    public float CarHorizontalMaxSpeed;
    public float CarAcceleration;
    public float CarDeceleration;
    public float CarMaxHealth;
    [Header("Horizontal Movement Cooldown")]
    public float minHorizontalMovementCooldown = 5;
    public float maxHorizontalMovementCooldown = 15;
    public float carHorizontalOffset = 4.5f;
}
