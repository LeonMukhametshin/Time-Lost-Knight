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

    private void UpdateAnimation()
    {
    }
}