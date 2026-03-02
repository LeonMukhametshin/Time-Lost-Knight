public class PlayerSecondaryAttackState : PlayerAttackState
{
    public PlayerSecondaryAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Player player,
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }
}