using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player m_player;
    [SerializeField] private Transform m_spawnPoint;

    private bool isPlayerSpawned;

    public void Spawn()
    {
        if(isPlayerSpawned)
        {
            return;
        }

        var player = GameObject.Instantiate(m_player, m_spawnPoint.position, Quaternion.identity, null);
        ServiceLocator.Register(player);
        BindCinemachine(player);

        isPlayerSpawned = true;
    }

    private static void BindCinemachine(Player player)
    {
        if (player == null)
            return;

        var cameras = Object.FindObjectsByType<CinemachineCamera>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None);

        if (cameras.Length == 0)
            return;

        var virtualCamera = cameras[0];
        virtualCamera.Follow = player.transform;
        if (virtualCamera.LookAt == null)
            virtualCamera.LookAt = player.transform;
    }
}
