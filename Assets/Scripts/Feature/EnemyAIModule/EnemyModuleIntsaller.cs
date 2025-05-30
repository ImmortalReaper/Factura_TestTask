using ShootingCar.AddressablesAddress;
using ShootingCar.Core.AssetLoader;
using ShootingCar.Core.Installer;
using ShootingCar.Feature.EnemyAIModule.Config;

namespace ShootingCar.Feature.EnemyAIModule
{
    public class EnemyModuleIntsaller : Installer<EnemyModuleIntsaller>
    {
        public override void InstallBindings()
        {
            IAddressablesAssetLoaderService addressablesAssetLoaderService = Container.Resolve<IAddressablesAssetLoaderService>();
            Container.Bind<EnemyConfigs>()
                .FromScriptableObject(addressablesAssetLoaderService.LoadAsset<EnemyConfigs>(Address.Configs.EnemyConfigs))
                .AsSingle();
            Container.Bind<IEnemyDataService>().To<EnemyDataService>().AsSingle();
        }
    }
}
