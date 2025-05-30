using DG.Tweening;
using ShootingCar.Feature.Health;
using UnityEngine;
using UnityEngine.UI;

namespace ShootingCar.Feature.UIModule.HealthBar
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
    
        private IEntityHealth _entityHealth;

        private void Awake()
        {
            _entityHealth = GetComponentInParent<IEntityHealth>();
            _entityHealth.OnHealthChange += UpdateHealth;
        }

        private void OnEnable() => UpdateHealth(_entityHealth.Health);

        private void OnDestroy()
        {
            _entityHealth.OnHealthChange += UpdateHealth;
        }
    
        private void UpdateHealth(float health)
        {
            healthBar.DOFillAmount(health / _entityHealth.MaxHealth, 0.2f).SetEase(Ease.InOutQuart);
        }
    }
}
