using UnityEngine;

public interface IControllable
{
    void Move(Vector2 x);
    void Jump();
    void Dash();
}