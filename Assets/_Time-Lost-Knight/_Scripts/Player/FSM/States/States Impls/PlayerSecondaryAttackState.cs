public class PlayerSecondaryAttackState : PlayerAttackState
{
    public PlayerSecondaryAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data, bool active) 
        : base(fsm, core,
            animBoolName, player, 
            data, active)
    {
    }
}