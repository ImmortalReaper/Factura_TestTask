using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CarMovement : MonoBehaviour
{
    private float _currentSpeed;
    private CancellationTokenSource _cts;
    private CarStatsConfig _carStatsConfig;
    
    public Action OnMaxSpeedReached;
    public Action OnStopped;
    
    [Inject]
    private void InjectDependencies(CarStatsConfig carStatsConfig)
    {
        _carStatsConfig = carStatsConfig;
    }

    private void Update()
    {
        transform.position += -transform.forward * (_currentSpeed * Time.deltaTime);
    }

    [ContextMenu("Start Acceleration")]
    public async UniTask StartAccelerationAsync()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        _currentSpeed = 0f;

        while (_currentSpeed < _carStatsConfig.CarMaxSpeed)
        {
            await UniTask.Yield(_cts.Token);

            _currentSpeed += _carStatsConfig.CarAcceleration * Time.deltaTime;
            _currentSpeed = Mathf.Min(_currentSpeed, _carStatsConfig.CarMaxSpeed);
        }
        
        OnMaxSpeedReached?.Invoke();
    }

    [ContextMenu("Stop Acceleration")]
    public async UniTask SmoothStopAsync()
    {
        CancelExisting();
        _cts = new CancellationTokenSource();
        
        while (_currentSpeed > 0f)
        {
            await UniTask.Yield(_cts.Token);
            _currentSpeed -= _carStatsConfig.CarDeceleration * Time.deltaTime;
            _currentSpeed = Mathf.Max(_currentSpeed, 0f);

            transform.position += -transform.forward * _currentSpeed * Time.deltaTime;
        }

        OnStopped?.Invoke();
    }
    
    private void CancelExisting()
    {
        if (_cts != null && !_cts.IsCancellationRequested)
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
    }
}
