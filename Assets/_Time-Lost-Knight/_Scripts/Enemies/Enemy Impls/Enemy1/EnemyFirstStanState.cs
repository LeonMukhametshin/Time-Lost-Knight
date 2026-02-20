public class EnemyFirstStanState : StanState
{
    private EnemyFirst enemy;

    public EnemyFirstStanState(EnemyFSM fsm, Entity entity, 
        string animBoolName, StunStateData data, EnemyFirst enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        this.enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if(!isStunTimeOver)
        {
            return; 
        }

        if (performCloseRangeAction)
        {
            fsm.SetState(enemy.meleeAttackState);
        }
        else if (isPlayerInMinAgroRange)
        {
            fsm.SetState(enemy.chargeState);
        }
        else
        {
            enemy.lookForPlayerState.SetTurnImmediately(true);
            fsm.SetState(enemy.lookForPlayerState);
        }
    }
}