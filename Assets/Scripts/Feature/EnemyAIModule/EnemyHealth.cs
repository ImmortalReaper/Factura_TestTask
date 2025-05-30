using System;
using DG.Tweening;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IEntityHealth
{
    [SerializeField] private CanvasGroup healthGroup;
    
    private float _health;
    private float _maxHealth;
    private bool _isHealthShown = false;
    private bool _isDead = false;
    
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public bool IsHealthShown => _isHealthShown;
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
        
        if(IsHealthShown is false)
            ShowHealth();
        
        OnHealthChange?.Invoke(_health);
        OnDamageTaken?.Invoke();
        if (contactData != null)
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
        healthGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutQuart);
        _isHealthShown = true;
    }

    public void HideHealth(bool immediate = false)
    {
        if (immediate)
        {
            healthGroup.alpha = 0;
            healthGroup.gameObject.SetActive(false);
            _isHealthShown = false;
            return;
        }
        healthGroup.DOFade(0, 1f).SetEase(Ease.InOutQuart)
            .OnComplete(() =>
            {
                healthGroup.gameObject.SetActive(false);
                _isHealthShown = false;
            });
    }
}
