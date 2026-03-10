using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    protected PlayerFSM m_playerFSM { get; private set; }

    private void Start()
    {
        m_playerFSM = ServiceLocator.Get<PlayerFSM>();
        if (m_playerFSM == null)
        {
            throw new System.Exception("PlayerFSM not found in ServiceLocator");
        }
    }


    public virtual void Unlock() { }
}