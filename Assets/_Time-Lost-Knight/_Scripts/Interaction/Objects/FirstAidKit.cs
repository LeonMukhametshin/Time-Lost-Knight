using UnityEngine;

public class FirstAidKit : MonoBehaviour 
{
    [SerializeField] private float m_healthPoints;
    [SerializeReferenceDropdown][SerializeReference] private IBuff buff;

    public void Heal()
    {
        //buff.Initialize();
        Destroy(gameObject);    
    } 
}