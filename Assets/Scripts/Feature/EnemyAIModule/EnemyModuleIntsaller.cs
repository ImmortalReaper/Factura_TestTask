using AddressablesAddress;
using Core.AssetLoader;
using Core.Installer;

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
