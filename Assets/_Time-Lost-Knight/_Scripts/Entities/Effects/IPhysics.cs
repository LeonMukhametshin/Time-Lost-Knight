using UnityEngine;

public interface IPhysics : IEffectable
{
    public void AddForce(Vector2 direction, ForceMode2D mode);
}