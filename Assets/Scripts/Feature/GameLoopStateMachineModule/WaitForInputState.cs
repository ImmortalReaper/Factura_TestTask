using ShootingCar.Core.Input;
using ShootingCar.Core.StateMachine;
using ShootingCar.Feature.CarModule;
using ShootingCar.Feature.PlayerData;
using UnityEngine;

namespace ShootingCar.Feature.GameLoopStateMachineModule
{
    public class WaitForInputState : IState
    {
        private GameLoopStateMachine _gameLoopStateMachine;
        private PlayerEntityModel _playerEntityModel;
        private IInputService _inputService;

        public WaitForInputState(IInputService inputService, 
            PlayerEntityModel playerEntityModel,
            GameLoopStateMachine gameLoopStateMachine)
        {
            _inputService = inputService;
            _playerEntityModel = playerEntityModel;
            _gameLoopStateMachine = gameLoopStateMachine;
        }
    
        public void Enter()
        {
            if (_playerEntityModel.PlayerEntity != null)
            {
                CarController carController = _playerEntityModel.PlayerEntity.GetComponent<CarController>();
                carController.CameraController.ActivateSideCamera();
            }
            _inputService.OnTapPerformed += OnTapPerformed;
        }

        public void Exit()
        {
            _inputService.OnTapPerformed -= OnTapPerformed;
        }
    
        private void OnTapPerformed(Vector2 position) =>
            _gameLoopStateMachine.ChangeState<GameplayState>();
    }
}
