using System;

namespace ShootingCar.Feature.BulletModule
{
    [Serializable]
    public class BulletData
    {
        public BulletType BulletType;
        public Bullet BulletPrefab;
        public float BulletSpeed;
        public float BulletDamage;
        public float BulletLifeTime;
    }
}
