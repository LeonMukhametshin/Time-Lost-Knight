public class EnemyDamageable : IDamageable, IEnemyBehaviuor
{
    private readonly IDamageable m_damageable;
    private readonly IEnemyBehaviuor m_enemyBehaviuor;

    public EnemyDamageable(IDamageable damageable, IEnemyBehaviuor enemyBehaviuor)
    {
        m_damageable = damageable;
        m_enemyBehaviuor = enemyBehaviuor;
    }

    public void TakeDamage(int amount) => 
        m_damageable.TakeDamage(amount);

    public void Move() => 
        m_enemyBehaviuor.Move();
}
