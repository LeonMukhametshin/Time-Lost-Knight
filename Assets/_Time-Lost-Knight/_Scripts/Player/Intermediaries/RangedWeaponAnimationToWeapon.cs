using Game.Weapons;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.Intermediaries
{
    [MovedFrom("")]
    public class RangedWeaponAnimationToWeapon : MonoBehaviour
    {
        [SerializeField] private RangedWeapon m_weapon;

        private void OnValidate()
        {
            if (m_weapon == null)
            {
                m_weapon = GetComponentInParent<RangedWeapon>();
            }
        }

        private void AnimationFinishTrigger() =>
            m_weapon?.AnimationFinishTrigger();

        public void AnimationActionTriger() =>
            m_weapon?.AnimationActionTriger();
    }
}
