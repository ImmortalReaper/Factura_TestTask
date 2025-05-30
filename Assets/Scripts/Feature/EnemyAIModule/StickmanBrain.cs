using System.Collections.Generic;
using Cinemachine;
using ShootingCar.Core.ObjectPool;
using ShootingCar.Feature.BulletModule;
using ShootingCar.Feature.CarModule;
using ShootingCar.Feature.EnemyAIModule.BehaviorStateMachine;
using ShootingCar.Feature.EnemyAIModule.Config;
using ShootingCar.Feature.PlayerData;
using UnityEngine;
using Zenject;

namespace ShootingCar.Feature.EnemyAIModule
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(FlashEffect))]
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class StickmanBrain : MonoBehaviour, IEnemy
    {
        [Header("Animator")]
        [SerializeField] private Animator stickmanAnimator;
        [SerializeField] private string stickmanHitParameter = "Hit";
        [Header("Components")]
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private FlashEffect flashEffect;
        [SerializeField] private Sensor playerSensor;
        [Header("Effects")]
        [SerializeField] private ParticleSystem bloodParticles;
        [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;

        private IEnemyDataService _enemyDataService;
        private StickmanStateMachine _stickmanStateMachine;
        private IObjectPoolService _objectPoolService;
        private PlayerEntityModel _playerEntityModel;
        private EnemyData _enemyData;
    
        public EnemyHealth EnemyHealth => enemyHealth;
        public Animator StickmanAnimator => stickmanAnimator;
        public EnemyData EnemyData => _enemyData;

        [Inject]
        private void InjectDependencies(PlayerEntityModel playerEntityModel, IEnemyDataService enemyDataService, IObjectPoolService objectPoolService)
        {
            _playerEntityModel = playerEntityModel;
            _enemyDataService = enemyDataService;
            _objectPoolService = objectPoolService;
        }
    
        private void Awake()
        {
            _enemyData = _enemyDataService.GetEnemyData(EnemyType.StandartStickman);
            InitializeStateMachine();
            SetDefaultState();
        }

        private void OnEnable()
        {
            playerSensor.OnTriggerSensorEnter += AttackPlayer;
            enemyHealth.OnContactDamageTaken += OnContactDamageTaken;
            enemyHealth.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            playerSensor.OnTriggerSensorEnter -= AttackPlayer;
            enemyHealth.OnContactDamageTaken -= OnContactDamageTaken;
            enemyHealth.OnDeath -= OnDeath;
        }

        private void OnValidate()
        {
            stickmanAnimator ??= GetComponent<Animator>();
            enemyHealth ??= GetComponent<EnemyHealth>();
            flashEffect ??= GetComponent<FlashEffect>();
            cinemachineImpulseSource ??= GetComponent<CinemachineImpulseSource>();
        }

        private void Update() => _stickmanStateMachine.GetCurrentState()?.Update();

        private void OnContactDamageTaken(ContactData contactData)
        {
            stickmanAnimator.SetTrigger(stickmanHitParameter);
            flashEffect.PlayFlashEffect();
            SpawnBlood(contactData.Point, contactData.Normal);
        }
    
        private void InitializeStateMachine()
        {
            _stickmanStateMachine = new StickmanStateMachine();
            IdleState idleState = new IdleState(stickmanAnimator);
            AttackState attackState = new AttackState(_playerEntityModel, this);
            _stickmanStateMachine.SetStates(new List<IBehaviorState> {idleState, attackState});
        }

        public void SetDefaultState()
        {
            enemyHealth.SetMaxHealth(_enemyData.EnemyMaxHealth);
            enemyHealth.SetHealth(_enemyData.EnemyMaxHealth);
            enemyHealth.HideHealth(true);
            _stickmanStateMachine.ChangeState<IdleState>();
        }

        private void OnDeath()
        {
            _objectPoolService.ReleaseTimed(gameObject);
        }
    
        public void SpawnBlood(Vector3 position, Vector3 normal)
        {
            ParticleSystem particleSystem = _objectPoolService.GetTimed(bloodParticles, 2f);
            particleSystem.transform.position = position;
            particleSystem.transform.LookAt(position + normal);
            particleSystem.Play();
        }
    
        private void AttackPlayer(Collider other)
        {
            if (other.CompareTag("Player"))
                _stickmanStateMachine.ChangeState<AttackState>();
        }
    
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CarController carController = other.gameObject.GetComponentInParent<CarController>();
                SpawnBlood(other.GetContact(0).point, other.GetContact(0).normal);
                carController.CarHealth.TakeDamage(_enemyData.EnemyDamage);
                cinemachineImpulseSource.GenerateImpulseWithForce(1f);
                _objectPoolService.ReleaseTimed(gameObject);
            }
        }
    }
}
