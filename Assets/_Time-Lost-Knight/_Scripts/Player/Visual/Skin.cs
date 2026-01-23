using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private PlayerMovementController m_playerMovementController;
    [SerializeField] private Animator m_animator;

    private void OnValidate()
    {
        if(!m_animator)
        {
            m_animator = GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        m_playerMovementController.StateChanged += UpdateAnimation;
    }

    private void OnDisable()
    {
        m_playerMovementController.StateChanged -= UpdateAnimation;
    }

    private void UpdateAnimation(MovementStates state)
    {
        switch(state)
        {
            case MovementStates.Idle:
                m_animator.SetTrigger("Idle");
                break;
            case MovementStates.Walk:
                m_animator.SetTrigger("Walk");
                break;
            case MovementStates.Dash:
                m_animator.SetTrigger("Dash");
                break;
            case MovementStates.Jump:
                m_animator.SetTrigger("Jump");
                break;
            case MovementStates.Fall:
                //TODO fall
                break;
            default:
                throw new System.Exception("There is no necessary Movement State");

        }
    }
}