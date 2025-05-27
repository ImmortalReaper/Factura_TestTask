using Addressables;
using Core.AssetLoader;
using Core.Installer;

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
