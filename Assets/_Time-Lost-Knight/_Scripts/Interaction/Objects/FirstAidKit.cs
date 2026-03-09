using UnityEngine;

public class FirstAidKit : MonoBehaviour 
{
    [SerializeField] private BuffEffect[] buff;

    private BuffContainer health;

    private void Start()
    {
        health = ServiceLocator
            .Get<Player>().gameObject
            .GetComponentInChildren<BuffContainer>();
    }

    public void Heal()
    {
        buff.ApplyEffect(health);
        Destroy(gameObject);    
    } 
}