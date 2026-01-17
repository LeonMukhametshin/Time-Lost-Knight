public class EnemyDamageable :  IEnemyBehaviuor
{
    private readonly IEnemyBehaviuor m_enemyBehaviuor;

    public EnemyDamageable(IEnemyBehaviuor enemyBehaviuor)
    {
        m_enemyBehaviuor = enemyBehaviuor;
    }

    public void Move() => 
        m_enemyBehaviuor.Move();
}
