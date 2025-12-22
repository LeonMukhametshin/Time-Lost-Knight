using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerContext context { get; private set; }

    private readonly List<IPlayerSystem> m_systems = new();

    private void Awake()
    {
        context = new PlayerContext();
    }

    public void RegisterSystem(IPlayerSystem system)
    {
        m_systems.Add(system);
        system.Initialize(context);
    }
}