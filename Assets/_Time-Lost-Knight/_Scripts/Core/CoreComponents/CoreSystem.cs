using Game.Buffs.Interfaces;
using Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.CoreComponents
{
    [MovedFrom("")]
    public class CoreSystem : MonoBehaviour
    {
        private readonly Dictionary<Type, CoreComponent> m_coreComponents = new();

        public readonly List<IEffectable> effectables = new();

        private List<IUpdate> m_updateComponents = new();

        public void Update()
        {
            foreach (var componetn in m_updateComponents)
            {
                componetn.Update();
            }
        }

        public void AddComponent(CoreComponent component)
        {
            var type = component.GetType();

            if (m_coreComponents.ContainsKey(type))
            {
                return;
            }

            m_coreComponents.Add(type, component);

            if (component is IEffectable effectable)
            {
                AddEffectableComponent(effectable);
            }

            if (component is IUpdate updateComponent)
            {
                AddUpdateComponent(updateComponent);
            }
        }

        private void AddEffectableComponent(IEffectable effectable)
        {
            if (effectables.Contains(effectable))
            {
                return;
            }

            effectables.Add(effectable);
        }

        private void AddUpdateComponent(IUpdate component)
        {
            if (m_updateComponents.Contains(component))
            {
                return;
            }

            m_updateComponents.Add(component);
        }


        public T GetCoreComponent<T>() where T : CoreComponent
        {
            if(!m_coreComponents.ContainsKey(typeof(T)))
            {
                CacheCoreComponents();
            }

            if (m_coreComponents.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }

            throw new Exception($"CoreSystem component of type {typeof(T)} not found.");
        }

        private void CacheCoreComponents()
        {
            var components = GetComponentsInChildren<CoreComponent>(true);

            foreach (var component in components)
            {
                AddComponent(component);
            }
        }
    }
}
