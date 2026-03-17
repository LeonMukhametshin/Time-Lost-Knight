using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player
{
    [MovedFrom("")]
    public class PlayerFactory : IPlayerFactory, IPlayerFactorySettings
    {
        private readonly string m_path;

        private PlayerController m_playerPrefab;
        private PlayerController m_playerInstance;

        Vector3 IPlayerFactorySettings.position { get; set; }

        public PlayerFactory(string path)
        {
            m_path = path;
        }

        public PlayerController Create()
        {
            if (m_playerInstance is not null)
            {
                return m_playerInstance;
            }

            if (m_playerPrefab is null)
            {
                var playerPrefab = Resources.Load<GameObject>(m_path);
                m_playerPrefab = playerPrefab.GetComponent<PlayerController>();
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
    [MovedFrom("")]
    public interface IPlayerFactorySettings
    {
        public Vector3 position { get; set; }
    }

    [MovedFrom("")]
    public interface IPlayerFactory
    {
        public PlayerController Create();
        public void Release();
    }
}
