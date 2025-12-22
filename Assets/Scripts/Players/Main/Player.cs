using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    public PlayerContext context { get; private set; }

    private void Awake()
    {
        context = new PlayerContext();
        if(instance == null) instance = this;
        else Destroy(this.gameObject);
    }
}