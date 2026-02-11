public class PlayerAbilytiState : PlayerState
{
    protected bool isAbilityDone;
    private bool m_isGrounded;

    public PlayerAbilytiState(Player player, PlayerFSM fsm, PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        m_isGrounded = player.collisionDetector.CheckGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Update()
    {
        base.Update();

        if(isAbilityDone)
        {
            if(m_isGrounded && player.movement.currentVelocity.y < 0.1f)
            {
                fsm.SetState(player.statesContainer.idleState);
            }
            else
            {
                fsm.SetState(player.statesContainer.airState);
            }
        }
    }
}