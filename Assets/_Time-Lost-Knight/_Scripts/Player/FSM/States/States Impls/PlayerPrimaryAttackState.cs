public class PlayerPrimaryAttackState : PlayerAttackState
{
    public PlayerPrimaryAttackState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }
}
