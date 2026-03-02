using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [field: SerializeField] public Animator animator { get; private set; }

    private EntityFSM fsm;

    public void Initialize(EntityFSM fsm)
    {
        this.fsm = fsm;
    }

    private void AnimationTrigger()
    {
        var state = fsm.currentState as PlayerState;
        state.TriggerAnimation();
    }

    private void AnimationFinishTrigger()
    {
        var state = fsm.currentState as PlayerState;
        state.FinishAnimation();
    }
}