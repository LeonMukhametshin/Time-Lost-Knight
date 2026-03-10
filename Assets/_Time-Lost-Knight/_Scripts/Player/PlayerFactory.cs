using UnityEngine;

public class PlayerFactory : IPlayerFactory, IPlayerFactorySettings
{
    private readonly string m_path;

    private Player m_playerPrefab;
    private Player m_playerInstance;

    Vector3 IPlayerFactorySettings.position { get; set; }

    public PlayerFactory(string path)
    {
        m_path = path;
    }

    public Player Create()
    {
        if (m_playerInstance is not null)
        {
            return m_playerInstance;
        }

        if (m_playerPrefab is null)
        {
            var playerPrefab = Resources.Load<GameObject>(m_path);
            m_playerPrefab = playerPrefab.GetComponent<Player>();
        }

        m_playerInstance = Object.Instantiate(m_playerPrefab, ((IPlayerFactorySettings)this).position, Quaternion.identity);

        return m_playerInstance;
    }

    public void Release()
    {
        Object.Destroy(m_playerInstance);
        m_playerInstance = null;
    }

}
public interface IPlayerFactorySettings
{
    public Vector3 position { get; set; }
}

public interface IPlayerFactory
{
    public Player Create();
    public void Release();
}