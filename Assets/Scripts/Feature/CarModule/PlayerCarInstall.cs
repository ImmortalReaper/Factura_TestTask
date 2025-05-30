using ShootingCar.AddressablesAddress;
using ShootingCar.Core.AssetLoader;
using ShootingCar.Core.Installer;

namespace ShootingCar.Feature.CarModule
{
    public class PlayerCarInstall : Installer<PlayerCarInstall>
    {
        public override void InstallBindings()
        {
            IAddressablesAssetLoaderService addressablesAssetLoaderService = Container.Resolve<IAddressablesAssetLoaderService>();
            Container.Bind<CarStatsConfig>()
                .FromScriptableObject(addressablesAssetLoaderService.LoadAsset<CarStatsConfig>(Address.Configs.CarStatsConfig))
                .AsSingle();
        }
    }
}
