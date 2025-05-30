using Core.Input;
using UnityEngine;
using Zenject;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _turretFirePoint;
    
    private BulletData _bulletData;
    private TurretData _turretData;
    private Camera _camera;
    private IInputService _inputService;
    private IObjectPoolService _objectPoolService;
    private IBulletDataService _bulletDataService;
    private ITurretDataService _turretDataService;
    private float _fireCooldown = 0;
    private bool _isFiring = false;
    private bool _isBarrelCanMove = false;
    
    [Inject]
    private void InjectDependencies(IInputService inputService, IObjectPoolService objectPoolService, IBulletDataService bulletDataService, ITurretDataService turretDataService)
    {
        _turretDataService = turretDataService;
        _inputService = inputService;
        _objectPoolService = objectPoolService;
        _bulletDataService = bulletDataService;
    }

    private void Awake()
    {
        _camera = Camera.main;
        _bulletData = _bulletDataService.GetBulletData(BulletType.StandardBullet);
        _turretData = _turretDataService.GetTurretConfig(TurretType.StandardTurret);
        _inputService.OnHeld += TurnTurretTowardsTarget;
    }

    private void OnDestroy()
    {
        _inputService.OnHeld -= TurnTurretTowardsTarget;
    }

    private void Update()
    {
        HandleShooting();
    }

    private void TurnTurretTowardsTarget(Vector2 position)
    {
        if(!_isBarrelCanMove) return;
        Ray ray = _camera.ScreenPointToRay(position);
        if (!Physics.Raycast(ray, out RaycastHit hit, 300f, _turretDataService.GetTargetLayerMask())) return;
        Vector3 lookDir = hit.point - _turret.position;
        lookDir.y = 0;
        _turret.localRotation = Quaternion.LookRotation(lookDir, Vector3.up);
    }

    private void HandleShooting()
    {
        if (!CanShoot()) 
            return;
        Shoot();
    }

    private bool CanShoot()
    {
        if(_isFiring is false) return false;
        
        _fireCooldown -= Time.deltaTime;
        if (_fireCooldown > 0f) 
            return false;
        _fireCooldown = _turretData.TurretFireRateInSec;
        return true;
    }
    
    [ContextMenu("Start Firing")]
    public void StartFiring() => _isFiring = true;
    [ContextMenu("Stop Firing")]
    public void StopFiring() => _isFiring = false;
    [ContextMenu("Barrel Movement Enable")]
    public void GunMovementEnable() => _isBarrelCanMove = true;
    [ContextMenu("Barrel Movement Disable")]
    public void GunMovementDisable() => _isBarrelCanMove = false;
    
    private void Shoot()
    {
        Bullet bullet = _objectPoolService.GetTimed(_bulletData.BulletPrefab, _bulletData.BulletLifeTime);
        bullet.transform.position = _turretFirePoint.position;
        bullet.transform.rotation = _turretFirePoint.rotation;
        if (bullet != null)
        {
            bullet.SetConfig(_bulletData);
            bullet.ClearTrail();
        }
    }
}
