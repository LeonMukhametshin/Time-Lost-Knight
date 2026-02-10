using UnityEngine;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private int m_capacity;

    private Queue<GameObject> m_queue = new();

    public static Pool Instance { get; private set; }

    private void Awake()
    {
        if(Instance is null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        GrowPool();
    }

    private void GrowPool()
    {
        for(int i = 0; i < m_capacity; i++)
        {
            var instance = GameObject.Instantiate(m_prefab);
            instance.transform.SetParent(transform);
            AddToPool(instance);    
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        m_queue.Enqueue(instance);
    }

    public GameObject Get()
    {
        if(m_queue.Count == 0)
        {
            GrowPool();
        }

        var instance = m_queue.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}