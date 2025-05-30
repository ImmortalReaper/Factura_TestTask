using Core.PrefabFactory;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Bootstraps
{
    [CreateAssetMenu(fileName = "GameplayBootstrap", menuName = "Installers/GameplayBootstrap")]
    public class GameplayBootstrap : ScriptableObjectInstaller<GameplayBootstrap>
    {
        public override void InstallBindings()
        {
            PrefabFactoryInstaller.Install(Container);
            UIModuleInstaller.Install(Container);
            EnemyModuleIntsaller.Install(Container);
            ObjectPoolInstaller.Install(Container);
            PlayerDataInstaller.Install(Container);
            PlayerCarInstall.Install(Container);
            GameLevelInstaller.Install(Container);
            GameLoopStateMachineInstaller.Install(Container);
            TurretInstaller.Install(Container);
            BulletInstaller.Install(Container);
            RoadGeneratorInstaller.Install(Container);
            WeatherInstaller.Install(Container);
        }
    }
}