using UnityEngine;

public interface ITurretDataService
{
    public TurretData GetTurretConfig(TurretType turretType);
    public LayerMask GetTargetLayerMask();
}
