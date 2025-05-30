using DG.Tweening;
using ShootingCar.Feature.EnemyAIModule;
using ShootingCar.Feature.GameLoopStateMachineModule;
using ShootingCar.Feature.TurretModule;
using UnityEngine;
using Zenject;

namespace ShootingCar.Feature.CarModule
{
    [RequireComponent(typeof(TurretController))]
    [RequireComponent(typeof(CarMovement))]
    [RequireComponent(typeof(CameraController))]
    [RequireComponent(typeof(CarHealth))]
    [RequireComponent(typeof(FlashEffect))]
    public class CarController : MonoBehaviour
    {
        [SerializeField] private TurretController turretController;
        [SerializeField] private CarMovement carMovement;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private CarHealth carHealth;
        [SerializeField] private ParticleSystem brokenCarEffect;
        [SerializeField] private FlashEffect flashEffect;
    
        private CarStatsConfig _carStatsConfig;
        private GameLoopStateMachine _gameLoopStateMachine;
    
        public Transform CarTransform => carMovement.CarTransform;
        public TurretController TurretController => turretController;
        public CarMovement CarMovement => carMovement;
        public CameraController CameraController => cameraController;
        public CarHealth CarHealth => carHealth;
        public FlashEffect FlashEffect => flashEffect;

        [Inject]
        private void InjectDependencies(CarStatsConfig carStatsConfig, GameLoopStateMachine gameLoopStateMachine)
        {
            _carStatsConfig = carStatsConfig;
            _gameLoopStateMachine = gameLoopStateMachine;
        }

        private void Start()
        {
            carMovement.SetCarStatsConfig(_carStatsConfig);
            carHealth.SetMaxHealth(_carStatsConfig.CarMaxHealth);
            carHealth.SetHealth(_carStatsConfig.CarMaxHealth);
            carHealth.OnDeath += OnDeath;
            carHealth.OnDamageTaken += OnDamageTaken;
        }

        private void OnDestroy()
        {
            carHealth.OnDeath += OnDeath;
            carHealth.OnDamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken()
        {
            flashEffect.PlayFlashEffect();
            CarTransform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 60);
        }

        private void OnValidate()
        {
            turretController ??= GetComponent<TurretController>();
            carMovement ??= GetComponent<CarMovement>();
            cameraController ??= GetComponent<CameraController>();
            carHealth ??= GetComponent<CarHealth>();
        }
    
        private void OnDeath()
        {
            brokenCarEffect.Play();
            _gameLoopStateMachine.ChangeState<LoseState>();
        }
    }
}
