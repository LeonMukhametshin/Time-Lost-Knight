using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    [SerializeField] protected Core core;

    public virtual void Awake()
    {
        //core.AddCoreComponent(this);
    }
}