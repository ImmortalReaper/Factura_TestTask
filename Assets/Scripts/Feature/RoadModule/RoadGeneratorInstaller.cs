using ShootingCar.AddressablesAddress;
using ShootingCar.Core.AssetLoader;
using ShootingCar.Core.Installer;

namespace ShootingCar.Feature.RoadModule
{
    public class RoadGeneratorInstaller : Installer<RoadGeneratorInstaller>
    {
        public override void InstallBindings()
        {
            IAddressablesAssetLoaderService addressablesAssetLoaderService = Container.Resolve<IAddressablesAssetLoaderService>();
            Container.Bind<RoadTypeConfig>()
                .WithId(Address.Configs.DesertRoadTypeConfig)
                .FromScriptableObject(addressablesAssetLoaderService.LoadAsset<RoadTypeConfig>(Address.Configs.DesertRoadTypeConfig))
                .AsSingle();
            Container.BindInterfacesAndSelfTo<RoadGeneratorService>().AsSingle().NonLazy();
        }
    }
}
