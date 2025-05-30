using ShootingCar.Core.AssetLoader;
using ShootingCar.Core.SceneLoader;
using UnityEngine;
using Zenject;

namespace ShootingCar.Bootstraps
{
    [CreateAssetMenu(fileName = "ProjectContext", menuName = "Installers/ProjectContext")]
    public class ProjectContext : ScriptableObjectInstaller<ProjectContext>
    {
        public override void InstallBindings()
        {
            AdressablesAssetLoaderInstaller.Install(Container);
            AddressablesSceneLoaderInstaller.Install(Container);
        }
    }
}