using Cysharp.Threading.Tasks;
using ShootingCar.AddressablesAddress;
using ShootingCar.Core.Input;
using ShootingCar.Core.StateMachine;
using ShootingCar.Feature.CarModule;
using ShootingCar.Feature.PlayerData;
using ShootingCar.Feature.UIModule;
using ShootingCar.Feature.UIModule.LoseUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingCar.Feature.GameLoopStateMachineModule
{
    public class LoseState : IState
    {
        private PlayerEntityModel _playerEntityModel;
        private IInputService _inputService;
        private IUIService _uiService;

        public LoseState(IInputService inputService, IUIService uiService, PlayerEntityModel playerEntityModel)
        {
            _inputService = inputService;
            _uiService = uiService;
            _playerEntityModel = playerEntityModel;
        }
    
        public void Enter()
        {
            _uiService.Show<LoseUI>();
            _inputService.OnTapPerformed += OnTapPerformed;
            if (_playerEntityModel.PlayerEntity != null)
            {
                CarController carController = _playerEntityModel.PlayerEntity.GetComponent<CarController>();
                carController.CarMovement.SmoothStopAsync().Forget();
                carController.TurretController.GunMovementDisable();
                carController.TurretController.StopFiring();
                carController.CarHealth.HideHealth();
            }
        }

        public void Exit() { }

        private void OnTapPerformed(Vector2 position)
        {
            _inputService.OnTapPerformed -= OnTapPerformed;
            SceneManager.LoadSceneAsync(Address.Scenes.Gameplay, LoadSceneMode.Single);
        }
    }
}
