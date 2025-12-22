using UnityEngine;

public class PlayerReceiver : MonoBehaviour, IReceiver
{
    private PlayerContext m_contex;

    private void Awake()
    {
        m_contex = GetComponent<Player>().context;
    }

    public void Receiver(int amout, string type)
    {
        m_contex.wallet.GetAccount(type).Add(amout);
    }
}