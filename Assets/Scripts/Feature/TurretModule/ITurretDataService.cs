using UnityEngine;

namespace ShootingCar.Feature.TurretModule
{
    public interface ITurretDataService
    {
        public TurretData GetTurretConfig(TurretType turretType);
        public LayerMask GetTargetLayerMask();
    }
}
