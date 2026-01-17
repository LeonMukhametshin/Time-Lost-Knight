public class EnemyAttack : IEnemyBehaviuor, IAttack
{
    private readonly IEnemyBehaviuor m_enemyBehaviuor;
    private readonly IAttack m_attack;

    public EnemyAttack(
        IEnemyBehaviuor enemyBehaviuor, 
        IAttack attack)
    {
        m_enemyBehaviuor = enemyBehaviuor;
        m_attack = attack;
    }

    public bool TryAttack(IDamageable damageable) =>
         m_attack.TryAttack(damageable);

    public void Move() => 
        m_enemyBehaviuor.Move();

}