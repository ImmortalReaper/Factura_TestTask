using Core.Installer;

public class PlayerDataInstaller : Installer<PlayerDataInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerEntityModel>().AsSingle();
    }
}
