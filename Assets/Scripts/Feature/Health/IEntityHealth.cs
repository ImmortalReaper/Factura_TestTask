using System;

public interface IEntityHealth
{
    public float Health { get; }
    public float MaxHealth { get; }
    public event Action<float> OnHealthChange;
    public event Action OnDamageTaken;
    public event Action<ContactData> OnContactDamageTaken;
    public event Action OnDeath;
    public void SetMaxHealth(float maxHealth);
    public void SetHealth(float health);
    public void TakeDamage(float damage, ContactData contactData = default);
}
