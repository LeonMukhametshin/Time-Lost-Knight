using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    [SerializeField] private Weapon m_weapon;

    private void OnValidate()
    {
        if(m_weapon is null)
        {
            m_weapon = GetComponentInParent<Weapon>();
        }
    }

    private void AnimationFinishTrigger() =>
        m_weapon?.AnimationFinishTrigger();

    private void AnimationStartMovementTrigger() =>
        m_weapon?.AnimatonStartMovementTrigger();

    private void AnimationStopMovementTrigger() =>
        m_weapon?.AnimatonStopMovementTrigger();

    private void AnimationTurnOffFlipTrigger() =>
        m_weapon?.AnimationTurnOffFlipTrigger();

    private void AnimationTurnOnFlipTrigger() =>
        m_weapon?.AnimationTurnOnFlipTrigger();

    public void AnimationActionTriger() =>
        m_weapon?.AnimationActionTriger();
}