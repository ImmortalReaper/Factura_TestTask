using ShootingCar.AddressablesAddress;
using ShootingCar.Core.AssetLoader;
using ShootingCar.Core.Installer;

namespace ShootingCar.Feature.BulletModule
{
    public class BulletInstaller : Installer<BulletInstaller>
    {
        public override void InstallBindings()
        {
            IAddressablesAssetLoaderService addressablesAssetLoaderService = Container.Resolve<IAddressablesAssetLoaderService>();
            Container.Bind<BulletsConfig>()
                .FromScriptableObject(addressablesAssetLoaderService.LoadAsset<BulletsConfig>(Address.Configs.BulletConfig))
                .AsSingle();
            Container.Bind<IBulletDataService>().To<BulletDataService>().AsSingle();
        }
    }
}
