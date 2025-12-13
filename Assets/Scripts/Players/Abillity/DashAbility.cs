using UnityEngine;

public class DashAbility : MonoBehaviour, IPlayerAbility
{
    public bool IsActive => true;

    public bool CanExecute => true;

    public void DoDash()
    {
        Debug.Log("Dash");
    }
}