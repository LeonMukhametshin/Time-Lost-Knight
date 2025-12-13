using UnityEngine;

public class WalkAbility : MonoBehaviour, IPlayerAbility
{
    public bool IsActive => true;

    public bool CanExecute => true;

    public void DoWalk(Vector3 direction)
    {
        if (!IsActive || !CanExecute || direction.magnitude < 0.0001f) return;

        Debug.Log("Walk " + direction);
    }
}