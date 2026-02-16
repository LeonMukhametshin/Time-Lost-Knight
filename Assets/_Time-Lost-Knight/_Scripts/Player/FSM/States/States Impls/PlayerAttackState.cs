using Unity.VisualScripting;

public class PlayerAttackState : PlayerAbilytiState
{
    private Weapon m_weapon;

    private float m_velocityToSet;
    private bool m_setVelocity;

    private int xInput;
    private bool checkShouldFlip;

    public PlayerAttackState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        m_setVelocity = false;
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
            core.flipController.CheckIfShoudFlip(xInput);
        }

        if (m_setVelocity)
        {
            core.movement.SetVelocityX(m_velocityToSet * core.collisionDetector.facingDirection);
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
        core.movement.SetVelocityX(velocity * core.collisionDetector.facingDirection);

        m_velocityToSet = velocity;
        m_setVelocity = true;
    }

    public void SetFlipCheck(bool value) =>
         checkShouldFlip = value;
}

public class PlayerPrimaryAttackState : PlayerAttackState
{
    public PlayerPrimaryAttackState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }
}

public class PlayerSecondaryAttackState : PlayerAttackState
{
    public PlayerSecondaryAttackState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName)
        : base(player, fsm, playerData, animBoolName)
    {
    }
}
