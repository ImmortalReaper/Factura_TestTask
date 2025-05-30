using UnityEngine;

public class TurretDataService : ITurretDataService
{
    private TurretConfig _turretConfig;

    public TurretDataService(TurretConfig turretConfig)
    {
        _turretConfig = turretConfig;
    }

    public TurretData GetTurretConfig(TurretType turretType) => _turretConfig.GetTurretConfig(turretType);
    public LayerMask GetTargetLayerMask() => _turretConfig.TargetLayer;
}
