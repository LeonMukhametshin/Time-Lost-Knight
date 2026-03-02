using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause instants { get; private set; }

    public bool isPaused { get; private set; }

    public List<IPauseHandler> m_pausedObjectst = new();

    private void Awake()
    {
        if (instants != null)
        {
            Destroy(this);
        }
        else
        {
            instants = this;
        }
    }

    private void OnDestroy()
    {
        if(instants == this)
        {
            instants = null;
        }
    }

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