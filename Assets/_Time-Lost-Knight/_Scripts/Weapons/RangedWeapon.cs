using Game.Player.FSM.States.Impls;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Weapons
{
    [MovedFrom("")]
    public class RangedWeapon : MonoBehaviour
    {
        [SerializeField] private Animator baseAnimator;
        [SerializeField] private Animator weaponAnimator;

        private PlayerRangedAttackState m_state;

        protected virtual void Awake() =>
            gameObject.SetActive(false);

        public void Initialize(PlayerRangedAttackState state) =>
            m_state = state;

        public void EnterWeapon()
        {
            gameObject.SetActive(true);

            if (baseAnimator != null)
            {
                baseAnimator.SetBool(WeaponAnimationConstants.ATTACK, true);
            }

            if (weaponAnimator != null)
            {
                weaponAnimator.SetBool(WeaponAnimationConstants.ATTACK, true);
            }
        }

        public void ExitWeapon()
        {
            if (baseAnimator != null)
            {
                baseAnimator.SetBool(WeaponAnimationConstants.ATTACK, false);
            }

            if (weaponAnimator != null)
            {
                weaponAnimator.SetBool(WeaponAnimationConstants.ATTACK, false);
            }

            gameObject.SetActive(false);
        }

        public void AnimationFinishTrigger() =>
            m_state?.FinishAnimation();

        public void AnimationActionTriger() =>
            m_state?.TriggerAnimation();
    }
}
