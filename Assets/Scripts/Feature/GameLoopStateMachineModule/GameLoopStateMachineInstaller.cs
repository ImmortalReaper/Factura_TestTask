using ShootingCar.Core.Installer;

namespace ShootingCar.Feature.GameLoopStateMachineModule
{
    public class GameLoopStateMachineInstaller : Installer<GameLoopStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameLoopStateMachine>().AsSingle();
            Container.Bind<WaitForInputState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
            Container.Bind<WinState>().AsSingle();
            Container.Bind<LoseState>().AsSingle();
        }
    }
}
