public class PlayerAttackState : PlayerAbilytiState
{
    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    private FlipContoller m_flipContoller;

    private Weapon m_weapon;

    private int xInput;
    private float m_velocityToSet;

    private bool setVelocity;
    private bool checkShouldFlip;

    public PlayerAttackState(Player player, EntityFSM fsm,
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
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
      
    public override void AnimationFinishTriger()
    {
        base.AnimationFinishTriger();

        isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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