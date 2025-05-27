public interface IEntityHealth
{
    public float Health { get; }
    public float MaxHealth { get; }
    public void SetMaxHealth(float maxHealth);
    public void SetHealth(float health);
    public void TakeDamage(float damage);
}
