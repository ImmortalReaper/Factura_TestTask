using AddressablesAddress;
using Core.AssetLoader;
using Core.Installer;

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
