using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private readonly HashSet<IObserver> m_observers = new HashSet<IObserver>();

    public void AddObserver(IObserver observer) =>
        m_observers.Add(observer);

    public void RemoveObserver(IObserver observer) =>
         m_observers.Remove(observer);

    public void NotifyObservers() 
    { 
        foreach (var observer in m_observers)
        {
            observer.Notify();
        }
    } 
}