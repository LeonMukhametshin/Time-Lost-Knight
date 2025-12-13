using UnityEngine;

public class JumpAbility : MonoBehaviour, IPlayerAbility
{
    public bool IsActive => true;

    public bool CanExecute => true;

    public void DoJump()
    {
        if (!IsActive || !CanExecute) return;

        Debug.Log("Jump");
    }
}