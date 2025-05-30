using AddressablesAddress;
using Core.AssetLoader;
using Core.Installer;

public class TurretInstaller : Installer<TurretInstaller>
{
    public override void InstallBindings()
    {
        IAddressablesAssetLoaderService addressablesAssetLoaderService = Container.Resolve<IAddressablesAssetLoaderService>();
        Container.Bind<TurretConfig>()
            .FromScriptableObject(addressablesAssetLoaderService.LoadAsset<TurretConfig>(Address.Configs.TurretConfig))
            .AsSingle();
        Container.Bind<ITurretDataService>().To<TurretDataService>().AsSingle();
    }
}
