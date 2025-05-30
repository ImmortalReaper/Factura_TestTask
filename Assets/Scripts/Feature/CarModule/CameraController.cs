using Cinemachine;
using UnityEngine;

namespace ShootingCar.Feature.CarModule
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _backCamera;
        [SerializeField] private CinemachineVirtualCamera _sideCamera;
    
        [SerializeField] private int _activePriority = 20;
        [SerializeField] private int _inactivePriority = 10;

        private bool _isBackActive = true;

        private void Awake()
        {
            ActivateBackCamera();
        }
    
        public void ActivateBackCamera()
        {
            _backCamera.Priority = _activePriority;
            _sideCamera.Priority = _inactivePriority;
            _isBackActive = true;
        }
    
        public void ActivateSideCamera()
        {
            _sideCamera.Priority = _activePriority;
            _backCamera.Priority = _inactivePriority;
            _isBackActive = false;
        }
    
        public void ToggleCamera()
        {
            if (_isBackActive) 
                ActivateSideCamera();
            else             
                ActivateBackCamera();
        }
    }
}
