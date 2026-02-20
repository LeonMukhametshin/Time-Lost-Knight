public class PlayerPrimaryAttackState : PlayerAttackState
{
    public PlayerPrimaryAttackState(Player player, EntityFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }
}