using Core.Installer;

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
