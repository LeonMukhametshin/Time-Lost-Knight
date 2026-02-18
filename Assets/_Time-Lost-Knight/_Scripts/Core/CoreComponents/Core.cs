using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> coreComponents = new();   
    private List<IUpdate> components = new();

    public void Update()
    {
        foreach (var componetn in components)
        {
            componetn.Update();
        }
    }

    public void AddUpdateComponent(IUpdate component)
    {
        if (components.Contains(component))
        {
            return;
        }

        components.Add(component);
    }

    public void AddCoreComponent(CoreComponent coreComponent)
    {
        if (coreComponents.Contains(coreComponent))
        {
            return;
        }

        coreComponents.Add(coreComponent);
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var component = coreComponents.OfType<T>().FirstOrDefault();

        if(component)
        {
            return component;
        }

        component = GetComponentInChildren<T>();

        if (component is null)
        {
            throw new System.Exception();
        }

        return component;
    }
}