public class PlayerAttackState : PlayerAbilytiState
{
    private Weapon m_weapon;

    private int xInput;
    private float m_velocityToSet;

    private bool setVelocity;
    private bool checkShouldFlip;

    public PlayerAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data, bool active) 
        : base(fsm, core, 
            animBoolName, player, 
            data, active)
    {
    }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;
        m_weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        m_weapon.ExitWeapon();
    }

    public override void Update()
    {
        base.Update();

        xInput = player.inputHandler.normalizedInputX;

        if(checkShouldFlip)
        {
            flipController.CheckIfShoudFlip(xInput);
        }

        if (setVelocity)
        {
            movement.SetVelocityX(m_velocityToSet * flipController.facingDirection);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        m_weapon = weapon;
        m_weapon.Initialize(this);
    }
      
    public override void FinishAnimation()
    {
        base.FinishAnimation();

        isAbilityDone = true;
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
    }

    public void SetPlayerVelocity(float velocity)
    {
        movement.SetVelocityX(velocity * flipController.facingDirection);

        m_velocityToSet = velocity;
        movement.canSetVelocity = true;
    }

    public void SetFlipCheck(bool value) =>
         checkShouldFlip = value;
}