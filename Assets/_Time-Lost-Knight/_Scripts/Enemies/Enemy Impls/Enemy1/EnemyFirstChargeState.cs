public class EnemyFirstChargeState : ChargeState
{
    private EnemyFirst enemy;

    public EnemyFirstChargeState(EnemyFSM fsm, Entity entity, 
        string animBoolName, ChargeStateData data, EnemyFirst enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        this.enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if (performCloseRangeAction)
        {
            fsm.SetState(enemy.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            fsm.SetState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if(isPlayerInMinAgroRange)
            {
                fsm.SetState(enemy.playerDetectedState);
            }
            else
            {
                fsm.SetState(enemy.lookForPlayerState);
            }
        } 
    }
}
