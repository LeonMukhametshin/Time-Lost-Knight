using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused { get; private set; }

    public List<IPauseHandler> m_pausedObjectst = new();

    public void SetPause(bool isEnable)
    {
        isPaused = isEnable;
        Notify();
    }

    public void Add(IPauseHandler handler) => 
        m_pausedObjectst.Add(handler);

    public void Remove(IPauseHandler handler) => 
        m_pausedObjectst.Remove(handler);

    private void Notify()
    {
        foreach(var handler in m_pausedObjectst)
        {
            handler?.IsPuased(isPaused);
        }
    }
}