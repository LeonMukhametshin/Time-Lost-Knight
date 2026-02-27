using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private float m_healthPoints;

    public void Heal()
    {
        ServiceLocator.Get<Player>().healthSystem.Heal(m_healthPoints);
        Destroy(gameObject);    
    } 
}