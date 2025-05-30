using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootingCar.Feature.CarModule
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private Transform carTransform;
    
        private float _currentSpeed;
        private CancellationTokenSource _accelerationToken;
        private CancellationTokenSource _horizontalMovementToken;
        private CarStatsConfig _carStatsConfig;
        private Tween _accelerationTween;
    
        public Transform CarTransform => carTransform;
        public Action OnMaxSpeedReached;
        public Action OnStopped;

        private void Awake()
        {
            OnMaxSpeedReached += StartHorizontalMovement;
            OnStopped += StopHorizontalMovement;
        }

        private void Update()
        {
            transform.position += -transform.forward * (_currentSpeed * Time.deltaTime);
        }
    
        private void OnDestroy()
        {
            OnMaxSpeedReached -= StartHorizontalMovement;
            OnStopped -= StopHorizontalMovement;
            _accelerationToken?.Cancel();
            _horizontalMovementToken?.Cancel();
        }
    
        public void SetCarStatsConfig(CarStatsConfig carStatsConfig) => _carStatsConfig = carStatsConfig;
    
        public void StartHorizontalMovement()
        {
            _horizontalMovementToken?.Cancel();
            _horizontalMovementToken = new CancellationTokenSource();
            MoveRandomlyAlongXWithEaseAsync(carTransform, 
                transform.position, 
                _carStatsConfig.carHorizontalOffset, 
                _carStatsConfig.CarHorizontalMaxSpeed,
                _carStatsConfig.minHorizontalMovementCooldown, 
                _carStatsConfig.maxHorizontalMovementCooldown, 
                _horizontalMovementToken.Token).Forget();
        }

        public void StopHorizontalMovement()
        {
            _horizontalMovementToken?.Cancel();
            _horizontalMovementToken?.Dispose();
        }

        private async UniTask MoveRandomlyAlongXWithEaseAsync(
            Transform transform,
            Vector3 center,
            float xOffset,
            float moveSpeed,
            float minWait,
            float maxWait,
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                float targetX = Random.Range(center.x - xOffset, center.x + xOffset);
                await MoveAlongXWithEaseAsync(transform, targetX, moveSpeed, cancellationToken);
                await WaitRandomTimeAsync(minWait, maxWait, cancellationToken);
            }
        }
    
        private async UniTask MoveAlongXWithEaseAsync(
            Transform transform,
            float targetX,
            float moveSpeed,
            CancellationToken cancellationToken)
        {
            float currentX = transform.position.x;
            float distance = Mathf.Abs(targetX - currentX);
            float duration = moveSpeed > 0f ? distance / moveSpeed : 0f;
        
            var uniTaskCompletionSource = new UniTaskCompletionSource();
        
            Tween tween = transform.DOMoveX(targetX, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => uniTaskCompletionSource.TrySetResult());
        
            cancellationToken.Register(() =>
            {
                tween.Kill();
                uniTaskCompletionSource.TrySetCanceled();
            });
        
            await uniTaskCompletionSource.Task;
        }
    
        private UniTask WaitRandomTimeAsync(
            float minWait,
            float maxWait,
            CancellationToken cancellationToken)
        {
            float waitTime = Random.Range(minWait, maxWait);
            return UniTask.Delay(
                TimeSpan.FromSeconds(waitTime),
                DelayType.DeltaTime,
                PlayerLoopTiming.Update,
                cancellationToken);
        }
    
        [ContextMenu("Start Acceleration")]
        public async UniTask StartAccelerationAsync()
        {
            CancelForwardMovement();
            _accelerationToken = new CancellationTokenSource();
            _currentSpeed = 0f;
        
            float accel = _carStatsConfig.CarAcceleration;
            float maxSpeed = _carStatsConfig.CarForwardMaxSpeed;
            float duration = accel > 0f ? maxSpeed / accel : 0f;
        
            _accelerationTween = DOTween.To(
                    () => _currentSpeed,
                    v => _currentSpeed = v,
                    maxSpeed,
                    duration)
                .SetEase(Ease.InOutQuad);
        
            await _accelerationTween.AsyncWaitForCompletion();
            OnMaxSpeedReached?.Invoke();
        }

        [ContextMenu("Stop Acceleration")]
        public async UniTask SmoothStopAsync()
        {
            CancelForwardMovement();
            _accelerationToken = new CancellationTokenSource();
            float decel = _carStatsConfig.CarDeceleration;
            float startSpeed = _currentSpeed;
            float duration = decel > 0f ? startSpeed / decel : 0f;

            _accelerationTween = DOTween.To(
                    () => _currentSpeed,
                    v => _currentSpeed = v,
                    0f,
                    duration)
                .SetEase(Ease.InOutQuad);

            await _accelerationTween.AsyncWaitForCompletion();
            OnStopped?.Invoke();
        }
    
        public void CancelForwardMovement()
        {
            if (_accelerationToken != null && !_accelerationToken.IsCancellationRequested)
            {
                _accelerationToken.Cancel();
                _accelerationToken.Dispose();
            }
            if (_accelerationTween != null && _accelerationTween.IsActive())
            {
                _accelerationTween.Kill();
                _accelerationTween = null;
            }
        }
    }
}
