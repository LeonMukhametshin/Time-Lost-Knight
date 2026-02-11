using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private StatesContainer m_container;

    public void Initialize(StatesContainer container) =>
        this.m_container = container;

    private void AnimationTrigger() =>
       m_container.fsm.currentState.AnimationTrigger();

    private void AnimationFinishTrigger() =>
        m_container.fsm.currentState.AnimationFinishTriger();
}