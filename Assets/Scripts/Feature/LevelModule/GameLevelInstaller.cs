using AddressablesAddress;
using Core.AssetLoader;
using Core.Installer;

public class GameLevelInstaller : Installer<GameLevelInstaller>
{
    public override void InstallBindings()
    {
        IAddressablesAssetLoaderService addressablesAssetLoaderService = Container.Resolve<IAddressablesAssetLoaderService>();
        Container.Bind<GameLevelsConfig>()
            .FromScriptableObject(addressablesAssetLoaderService.LoadAsset<GameLevelsConfig>(Address.Configs.GameLevelsConfig))
            .AsSingle();
        Container.BindInterfacesAndSelfTo<EnemySpawnService>().AsSingle();
        Container.Bind<ILevelConfigService>().To<LevelConfigService>().AsSingle();
        Container.BindInterfacesAndSelfTo<DistanceService>().AsSingle();
    }
}
