public class EnemyAttack : IDamageable, IEnemyBehaviuor, IAttack
{
    private readonly IDamageable m_damageable;
    private readonly IEnemyBehaviuor m_enemyBehaviuor;
    private readonly IAttack m_attack;

    public EnemyAttack(
        IDamageable damageable, 
        IEnemyBehaviuor enemyBehaviuor, 
        IAttack attack)
    {
        m_damageable = damageable;
        m_enemyBehaviuor = enemyBehaviuor;
        m_attack = attack;
    }
    public void TakeDamage(int amount) => 
        m_damageable.TakeDamage(amount);

    public bool TryAttack(IDamageable damageable) =>
         m_attack.TryAttack(damageable);

    public void Move() => 
        m_enemyBehaviuor.Move();

}