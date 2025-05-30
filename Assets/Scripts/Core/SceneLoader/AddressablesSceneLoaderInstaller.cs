using Core.Installer;

public class AddressablesSceneLoaderInstaller : Installer<AddressablesSceneLoaderInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IAddressablesSceneLoaderService>().To<AddressablesSceneLoaderService>().AsSingle();
    }
}
