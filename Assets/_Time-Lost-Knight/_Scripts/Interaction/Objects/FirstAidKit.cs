using UnityEngine;

public class FirstAidKit : MonoBehaviour 
{
    [SerializeField] private BuffEffect[] buff;

    private BuffContainer health;

    private void Start()
    {
        health = ServiceLocator
            .Get<IPlayerFactory>().Create()
            .GetComponent<BuffContainer>();
    }

    public void Heal()
    {
        buff.ApplyEffect(health);
        Destroy(gameObject);    
    } 
}