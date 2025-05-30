using AddressablesAddress;
using Core.AssetLoader;
using Core.Installer;

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
