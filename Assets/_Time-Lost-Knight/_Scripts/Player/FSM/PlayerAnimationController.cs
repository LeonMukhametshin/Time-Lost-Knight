using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [field: SerializeField] public Animator animator { get; private set; }
    private StatesContainer m_container;

    public void Initialize(StatesContainer container)
    {
        m_container = container;
    }

    private void AnimationTrigger() =>
       m_container.fsm.currentState.AnimationTrigger();

    private void AnimationFinishTrigger() =>
        m_container.fsm.currentState.AnimationFinishTriger();
}