namespace ShootingCar.Feature.BulletModule
{
    public class BulletDataService : IBulletDataService
    {
        private BulletsConfig _bulletsConfig;

        public BulletDataService(BulletsConfig bulletsConfig)
        {
            _bulletsConfig = bulletsConfig;
        }

        public BulletData GetBulletData(BulletType bulletType) => _bulletsConfig.GetBulletConfig(bulletType);
    }
}
