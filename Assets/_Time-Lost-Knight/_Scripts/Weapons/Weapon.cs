using Game.Core.CoreComponents;
using Game.Player.FSM.States.Impls;
using Game.ScriptableObjects.Weapons;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Weapons
{
    [MovedFrom("")]
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData weaponData;
        [SerializeField] protected CoreSystem core;

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

            baseAnimator.SetBool(WeaponAnimationConstants.ATTACK, true);
            weaponAnimator.SetBool(WeaponAnimationConstants.ATTACK, true);

            baseAnimator.SetInteger(WeaponAnimationConstants.ATTACK_COUNTER, attackCounter);
            weaponAnimator.SetInteger(WeaponAnimationConstants.ATTACK_COUNTER, attackCounter);
        }

        public virtual void ExitWeapon()
        {
            baseAnimator.SetBool(WeaponAnimationConstants.ATTACK, false);
            weaponAnimator.SetBool(WeaponAnimationConstants.ATTACK, false);

            attackCounter++;

            gameObject.SetActive(false);
        }

        public virtual void AnimationFinishTrigger() =>
            state.FinishAnimation();

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
}

