using ShootingCar.Core.Installer;

namespace ShootingCar.Core.ObjectPool
{
    public class ObjectPoolInstaller : Installer<ObjectPoolInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObjectPoolSystem>().AsSingle();
        }
    }
}
