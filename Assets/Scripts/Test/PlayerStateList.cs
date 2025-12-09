using UnityEngine;

public class PlayerStateList : MonoBehaviour
{
    public bool Jumping { get; set;}
    public bool Dashing { get; set;}
    public bool LookingRight {get; set;}
    public bool Invincible {get; set;}

    public bool RecoilingX;
    public bool RecoilingY;

}