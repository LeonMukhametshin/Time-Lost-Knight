using System;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private readonly Dictionary<Type, CoreComponent> coreComponents = new();
    private List<IUpdate> updateComponents = new();

    public void Update()
    {
        foreach (var componetn in updateComponents)
        {
            componetn.Update();
        }
    }

    public void AddCoreComponent(CoreComponent component)
    {
        var type = component.GetType();

        if (coreComponents.ContainsKey(type))
        {
            return;
        }

        coreComponents.Add(type, component);

        if (component is IUpdate updateComponent)
        {
            AddUpdateComponent(updateComponent);
        }
    }

    private void AddUpdateComponent(IUpdate component)
    {
        if (updateComponents.Contains(component))
        {
            return;
        }

        updateComponents.Add(component);
    }


    public T GetCoreComponent<T>() where T : CoreComponent
    { 
        if(!coreComponents.ContainsKey(typeof(T)))
        {
            CacheCoreComponents();
        }

        if (coreComponents.TryGetValue(typeof(T), out var component))
        {
            return component as T;
        }

        throw new Exception($"Core component of type {typeof(T)} not found.");
    }

    private void CacheCoreComponents()
    {
        var components = GetComponentsInChildren<CoreComponent>(true);

        foreach (var component in components)
        {
            AddCoreComponent(component);
        }
    }
}