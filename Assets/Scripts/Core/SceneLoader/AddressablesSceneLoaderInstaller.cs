using ShootingCar.Core.Installer;

namespace ShootingCar.Core.SceneLoader
{
    public class AddressablesSceneLoaderInstaller : Installer<AddressablesSceneLoaderInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IAddressablesSceneLoaderService>().To<AddressablesSceneLoaderService>().AsSingle();
        }
    }
}
