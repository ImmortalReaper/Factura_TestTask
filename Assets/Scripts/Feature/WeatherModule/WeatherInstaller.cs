using AddressablesAddress;
using Core.AssetLoader;
using Core.Installer;

public class WeatherInstaller : Installer<WeatherInstaller>
{
    public override void InstallBindings()
    {
        IAddressablesAssetLoaderService addressablesAssetLoaderService = Container.Resolve<IAddressablesAssetLoaderService>();
        Container.Bind<WeatherConfigs>()
            .FromScriptableObject(addressablesAssetLoaderService.LoadAsset<WeatherConfigs>(Address.Configs.WeatherConfigs))
            .AsSingle();
        Container.Bind<IWeatherDataService>().To<WeatherDataService>().AsSingle();
        Container.BindInterfacesAndSelfTo<WeatherService>().AsSingle().NonLazy();
    }
}
