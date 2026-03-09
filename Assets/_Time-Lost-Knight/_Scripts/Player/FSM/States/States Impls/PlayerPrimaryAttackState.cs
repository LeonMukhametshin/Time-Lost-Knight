public class PlayerPrimaryAttackState : PlayerAttackState
{
    public PlayerPrimaryAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data, bool active) 
        : base(fsm, core, 
            animBoolName, player, 
            data, active)
    {
    }
}