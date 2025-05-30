using Cysharp.Threading.Tasks;

public class GameplayState : IState
{
    private PlayerEntityModel _playerEntityModel;
    private ILevelConfigService _levelConfigService;
    private IEnemySpawnService _enemySpawnService;
    private IDistanceService _distanceService;
    private GameLoopStateMachine _gameLoopStateMachine;

    public GameplayState(PlayerEntityModel playerEntityModel, 
        ILevelConfigService levelConfigService, 
        IEnemySpawnService enemySpawnService, 
        IDistanceService distanceService, 
        GameLoopStateMachine gameLoopStateMachine)
    {
        _playerEntityModel = playerEntityModel;
        _levelConfigService = levelConfigService;
        _enemySpawnService = enemySpawnService;
        _distanceService = distanceService;
        _gameLoopStateMachine = gameLoopStateMachine;
    }
    
    public void Enter()
    {
        LevelConfig level = _levelConfigService.GetLevelConfig(0);
        _enemySpawnService.StartSpawning(level);
        _distanceService.StartTracking(level);
        _distanceService.OnLevelCompleted += OnLevelCompleted;
        if (_playerEntityModel.PlayerEntity != null)
        {
            CarController carController = _playerEntityModel.PlayerEntity.GetComponent<CarController>();
            carController.CameraController.ActivateBackCamera();
            carController.CarMovement.StartAccelerationAsync().Forget();
            carController.TurretController.GunMovementEnable();
            carController.TurretController.StartFiring();
            carController.CarHealth.ShowHealth();
        }
    }

    public void Exit()
    {
        _distanceService.OnLevelCompleted -= OnLevelCompleted;
        _enemySpawnService.StopSpawning();
        _distanceService.StopTracking();
    }

    private void OnLevelCompleted()
    {
        _gameLoopStateMachine.ChangeState<WinState>();
    }
}
