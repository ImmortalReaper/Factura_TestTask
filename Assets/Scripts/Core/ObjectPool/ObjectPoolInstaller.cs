using Core.Installer;

public class ObjectPoolInstaller : Installer<ObjectPoolInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ObjectPoolSystem>().AsSingle();
    }
}
