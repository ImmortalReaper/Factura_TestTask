using UnityEngine;

[RequireComponent(typeof(TurretController))]
[RequireComponent(typeof(CarMovement))]
public class CarController : MonoBehaviour
{
    [SerializeField] private TurretController turretController;
    [SerializeField] private CarMovement carMovement;
    
    public TurretController TurretController => turretController;
    public CarMovement CarMovement => carMovement;
}
