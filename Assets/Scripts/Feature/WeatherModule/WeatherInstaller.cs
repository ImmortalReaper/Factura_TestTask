using ShootingCar.AddressablesAddress;
using ShootingCar.Core.AssetLoader;
using ShootingCar.Core.Installer;

namespace ShootingCar.Feature.WeatherModule
{
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
}
