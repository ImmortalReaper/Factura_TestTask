using ShootingCar.Core.Installer;

namespace ShootingCar.Feature.PlayerData
{
    public class PlayerDataInstaller : Installer<PlayerDataInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerEntityModel>().AsSingle();
        }
    }
}
