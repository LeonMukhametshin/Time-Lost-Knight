using UnityEngine;

public interface IPhysics
{
    public void AddForce(Vector2 direction, ForceMode2D mode);
}