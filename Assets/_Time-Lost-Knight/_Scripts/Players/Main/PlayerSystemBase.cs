using UnityEngine;

public abstract class PlayerSystemBase : MonoBehaviour, IPlayerSystem
{
    protected PlayerContext context { get; private set; }

    protected virtual void Start()
    {
        var player = GetComponent<Player>();
        player.RegisterSystem(this);
    }

    public void Initialize(PlayerContext playerContex)
    {
        context = playerContex;
        OnInitialize();
    }

    protected abstract void OnInitialize();
}