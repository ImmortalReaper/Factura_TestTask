public class EntityHealth : IEntityHealth
{
    private float _health;
    private float _maxHealth;
    
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    
    public void SetMaxHealth(float maxHealth) => _maxHealth = maxHealth;
    public void SetHealth(float health) => _health = health;
    public void TakeDamage(float damage)
    {
        if (_health - damage > 0)
            _health -= damage;
        else
            _health = 0;
    }
}
