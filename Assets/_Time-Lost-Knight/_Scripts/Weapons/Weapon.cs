using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    
    [SerializeField] protected Animator baseAnimator;
    [SerializeField] protected Animator weaponAnimator;

    protected PlayerAttackState state;

    protected int attackCounter;

    public void Initialize(PlayerAttackState state) =>
        this.state = state;

    protected virtual void Awake() =>
        gameObject.SetActive(false);

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }

        baseAnimator.SetBool(WeaponAnimationÑonstants.ATTACK, true);
        weaponAnimator.SetBool(WeaponAnimationÑonstants.ATTACK, true);

        baseAnimator.SetInteger(WeaponAnimationÑonstants.ATTACK_COUNTER, attackCounter);
        weaponAnimator.SetInteger(WeaponAnimationÑonstants.ATTACK_COUNTER, attackCounter);
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool(WeaponAnimationÑonstants.ATTACK, false);
        weaponAnimator.SetBool(WeaponAnimationÑonstants.ATTACK, false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    public virtual void AnimationFinishTrigger() =>
        state.AnimationFinishTriger();

    public virtual void AnimatonStartMovementTrigger() =>
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);

    public virtual void AnimatonStopMovementTrigger() =>
         state.SetPlayerVelocity(0f);

    public virtual void AnimationTurnOffFlipTrigger() =>
        state.SetFlipCheck(false);

    public virtual void AnimationTurnOnFlipTrigger() =>
        state.SetFlipCheck(true);

    public virtual void AnimationActionTriger() { }
}