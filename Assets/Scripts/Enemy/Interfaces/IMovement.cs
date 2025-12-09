using UnityEngine;

public interface IMovement 
{
    void Update();
    void Move(Vector2 direction);
    void Stop();
}
