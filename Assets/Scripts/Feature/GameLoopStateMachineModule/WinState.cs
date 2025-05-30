using Cysharp.Threading.Tasks;
using ShootingCar.AddressablesAddress;
using ShootingCar.Core.Input;
using ShootingCar.Core.SceneLoader;
using ShootingCar.Core.StateMachine;
using ShootingCar.Feature.CarModule;
using ShootingCar.Feature.PlayerData;
using ShootingCar.Feature.UIModule;
using ShootingCar.Feature.UIModule.WinUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingCar.Feature.GameLoopStateMachineModule
{
    public class WinState : IState
    {
        private PlayerEntityModel _playerEntityModel;
        private IInputService _inputService;
        private IUIService _uiService;

        public WinState(IInputService inputService, IUIService uiService, PlayerEntityModel playerEntityModel, IAddressablesSceneLoaderService addressablesSceneLoaderService)
        {
            _inputService = inputService;
            _uiService = uiService;
            _playerEntityModel = playerEntityModel;
        }
    
        public void Enter()
        {
            _uiService.Show<WinUI>();
            _inputService.OnTapPerformed += OnTapPerformed;
            if (_playerEntityModel.PlayerEntity != null)
            {
                CarController carController = _playerEntityModel.PlayerEntity.GetComponent<CarController>();
                carController.CarMovement.SmoothStopAsync().Forget();
                carController.CarMovement.StopHorizontalMovement();
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
