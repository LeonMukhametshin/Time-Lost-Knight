public class PlayerFSM
{
    public PlayerState currentState { get; private set; }

    private bool m_isInitialized;

    public void Initialize(PlayerState state)
    {
        if (m_isInitialized)
        {
            return;
        }

        currentState = state;
        currentState.Enter();

        m_isInitialized = true;
    }

    public void SetState(PlayerState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void Update()
    {
        if(!m_isInitialized)
        {
            return;
        }

        currentState.Update();
    }
       

    public void FixedUpdate()
    {
        if (!m_isInitialized)
        {
            return;
        }

        currentState.FixedUpdate();
    }
}