using UnityEngine;

public class CharacterInputControllerObserver : MonoBehaviour
{
    [SerializeField] private WalkAbility m_walkAbility;
    [SerializeField] private JumpAbility m_jumpAbility;
    [SerializeField] private DashAbility m_dashAbility;

    [SerializeField] private CharacterInputController m_characterInputController;

    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
        m_characterInputController.Move -= m_walkAbility.DoWalk;
        m_characterInputController.Jump -= m_jumpAbility.DoJump;
        m_characterInputController.Dash -= m_dashAbility.DoDash;
    }

    private void Awake()
    {
        m_walkAbility = GetComponent<WalkAbility>();
        m_jumpAbility = GetComponent<JumpAbility>();
        m_dashAbility = GetComponent<DashAbility>();
        m_characterInputController = GetComponent<CharacterInputController>();

        Initialize();
    }

    private void Initialize()
    {
        m_characterInputController.Move += m_walkAbility.DoWalk;
        m_characterInputController.Jump += m_jumpAbility.DoJump;
        m_characterInputController.Dash += m_dashAbility.DoDash;
    }
}