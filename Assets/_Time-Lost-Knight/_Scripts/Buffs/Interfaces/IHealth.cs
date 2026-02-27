public interface IHealth
{
    public float value { get; }

    public void TakeDamage(float damage);
    public void Heal(float heal);
}