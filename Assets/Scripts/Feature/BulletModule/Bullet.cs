using ShootingCar.Core.ObjectPool;
using ShootingCar.Feature.EnemyAIModule;
using UnityEngine;
using Zenject;

namespace ShootingCar.Feature.BulletModule
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private Sensor _bulletSensor;
    
        private float _bulletSpeed;
        private float _bulletDamage;
        private float _bulletLifeTime;
        private IObjectPoolService _objectPoolService;
    
        public Transform Transform => transform;
        public float Damage => _bulletDamage;
    
        [Inject]
        private void InjectDependencies(IObjectPoolService objectPoolService)
        {
            _objectPoolService = objectPoolService;
        }
    
        public void SetConfig(BulletData config)
        {
            _bulletSpeed = config.BulletSpeed;
            _bulletDamage = config.BulletDamage;
            _bulletLifeTime = config.BulletLifeTime;
        }

        private void Awake()
        {
            _bulletSensor.OnTriggerSensorEnter += OnTriggerEnter;
        }

        private void OnDestroy()
        {
            _bulletSensor.OnTriggerSensorEnter -= OnTriggerEnter;
        }

        private void Update()
        {
            transform.position += transform.forward * (_bulletSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                ContactData contactData = new ContactData(other.ClosestPoint(transform.position), transform.forward);
                enemyHealth.TakeDamage(_bulletDamage, contactData);
            }
            _objectPoolService.ReleaseTimed(gameObject);
        }

        public void ClearTrail() => _trailRenderer.Clear();
    }
}
