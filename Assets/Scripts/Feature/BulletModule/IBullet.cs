using UnityEngine;

namespace ShootingCar.Feature.BulletModule
{
    public interface IBullet
    {
        public Transform Transform { get; }
        public float Damage { get; }
        public void SetConfig(BulletData config);
        public void ClearTrail();
    }
}
