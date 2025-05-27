using Core.Installer;

public class WeatherInstaller : Installer<WeatherInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<WeatherService>().AsSingle().NonLazy();
    }
}
