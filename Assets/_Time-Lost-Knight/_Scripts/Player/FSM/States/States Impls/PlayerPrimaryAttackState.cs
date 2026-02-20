public class PlayerPrimaryAttackState : PlayerAttackState
{
    public PlayerPrimaryAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }
}