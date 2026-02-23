using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ServiceLocator 
{
    private static ServiceLocator m_serviceLocator;

    private Dictionary<Type, object> m_services = new();
    
    public static void Register<T>(T newService) where T : class
    {
        m_serviceLocator ??= new ServiceLocator();

        if (m_serviceLocator.m_services.ContainsKey(typeof(T)))
        {
            m_serviceLocator.m_services.Remove(typeof(T));
        }

        m_serviceLocator.m_services.Add(typeof(T), newService);
    }

    public static T Get<T>() where T : class
    {
        if (m_serviceLocator is null)
        {
            throw new NullReferenceException("Service locator is null");
        }

        return m_serviceLocator.m_services[typeof(T)] as T;
    }
}