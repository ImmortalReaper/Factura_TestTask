using AddressablesAddress;
using Core.Input;
using Cysharp.Threading.Tasks;
using Feature.UI;
using UnityEngine;

public class WinState : IState
{
    private PlayerEntityModel _playerEntityModel;
    private IInputService _inputService;
    private IUIService _uiService;
    private IAddressablesSceneLoaderService _addressablesSceneLoaderService;

    public WinState(IInputService inputService, IUIService uiService, PlayerEntityModel playerEntityModel, IAddressablesSceneLoaderService addressablesSceneLoaderService)
    {
        _inputService = inputService;
        _uiService = uiService;
        _playerEntityModel = playerEntityModel;
        _addressablesSceneLoaderService = addressablesSceneLoaderService;
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

    public void Exit()
    {
        _inputService.OnTapPerformed -= OnTapPerformed;
    }

    private void OnTapPerformed(Vector2 position)
    {
        _addressablesSceneLoaderService.LoadSceneAsync(Address.Scenes.Gameplay);
    }
}
