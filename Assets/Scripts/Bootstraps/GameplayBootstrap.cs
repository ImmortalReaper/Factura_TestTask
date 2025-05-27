using Core.PrefabFactory;
using UnityEngine;
using Zenject;

namespace Bootstraps
{
    [CreateAssetMenu(fileName = "GameplayBootsrap", menuName = "Installers/GameplayBootsrap")]
    public class GameplayBootstrap : ScriptableObjectInstaller<GameplayBootstrap>
    {
        public override void InstallBindings()
        {
            PrefabFactoryInstaller.Install(Container);
            ObjectPoolInstaller.Install(Container);
            PlayerDataInstaller.Install(Container);
            PlayerCarInstall.Install(Container);
            RoadGeneratorInstaller.Install(Container);
            WeatherInstaller.Install(Container);
        }
    }
}