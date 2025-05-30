using System;
using DG.Tweening;
using ShootingCar.Feature.BulletModule;
using ShootingCar.Feature.Health;
using UnityEngine;

namespace ShootingCar.Feature.CarModule
{
    public class CarHealth : MonoBehaviour, IEntityHealth
    {
        [SerializeField] private CanvasGroup healthGroup;
    
        private float _health;
        private float _maxHealth;
        private bool _isDead;
    
        public float MaxHealth => _maxHealth;
        public float Health => _health;
        public bool IsDead => _isDead;
        public event Action<float> OnHealthChange;
        public event Action OnDamageTaken;
        public event Action<ContactData> OnContactDamageTaken;
        public event Action OnDeath;

        public void SetMaxHealth(float maxHealth) => _maxHealth = maxHealth;
        public void SetHealth(float health)
        {
            if (health <= 0)
            {
                _health = 0;
                return;
            }
        
            if(health > _maxHealth)
                _health = _maxHealth;
            else
                _health = health;
        
            if(_isDead)
                _isDead = false;
            OnHealthChange?.Invoke(health);
        }

        public void TakeDamage(float damage, ContactData contactData = null)
        {
            if (_health - damage > 0)
                _health -= damage;
            else
                _health = 0;
        
            OnHealthChange?.Invoke(_health);
            OnDamageTaken?.Invoke();
            if(contactData != null)
                OnContactDamageTaken?.Invoke(contactData);
            if (_health == 0)
            {
                OnDeath?.Invoke();
                _isDead = true;
            }
        }
    
        public void ShowHealth()
        {
            healthGroup.gameObject.SetActive(true);
            healthGroup.DOFade(1, 2f).SetEase(Ease.InOutQuart);
        }

        public void HideHealth()
        {
            healthGroup.DOFade(0, 1f).SetEase(Ease.InOutQuart)
                .OnComplete(() => healthGroup.gameObject.SetActive(false));
        }
    }
}

