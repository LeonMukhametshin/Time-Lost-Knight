using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator { get; private set; }
    private StatesContainer m_container;

    public void Initialize(Animator animator, StatesContainer container)
    {
        m_container = container;
        this.animator = animator;
    }

    private void AnimationTrigger() =>
       m_container.fsm.currentState.AnimationTrigger();

    private void AnimationFinishTrigger() =>
        m_container.fsm.currentState.AnimationFinishTriger();
}