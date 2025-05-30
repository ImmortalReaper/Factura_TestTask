using ShootingCar.Core.ObjectPool;
using ShootingCar.Core.PrefabFactory;
using ShootingCar.Feature.BulletModule;
using ShootingCar.Feature.CarModule;
using ShootingCar.Feature.EnemyAIModule;
using ShootingCar.Feature.GameLoopStateMachineModule;
using ShootingCar.Feature.LevelModule;
using ShootingCar.Feature.PlayerData;
using ShootingCar.Feature.RoadModule;
using ShootingCar.Feature.TurretModule;
using ShootingCar.Feature.UIModule;
using ShootingCar.Feature.WeatherModule;
using UnityEngine;
using Zenject;

namespace ShootingCar.Bootstraps
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