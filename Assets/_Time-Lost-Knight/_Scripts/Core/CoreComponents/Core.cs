using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> coreComponents = new();   
    private List<IUpdate> updateComponents = new();

    public void Update()
    {
        foreach (var componetn in updateComponents)
        {
            componetn.Update();
        }
    }

    public void AddUpdateComponent(IUpdate component)
    {
        if (updateComponents.Contains(component))
        {
            return;
        }

        updateComponents.Add(component);
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