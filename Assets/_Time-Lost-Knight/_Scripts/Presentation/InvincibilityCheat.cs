using Game.Core.CoreComponents;
using Game.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Presentation
{
    [MovedFrom("")]
    public class InvincibilityCheat : MonoBehaviour
    {
        private const string ROOT_NAME = "[INVINCIBILITY CHEAT]";

        private PlayerController m_player;
        private HealthComponent m_health;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            if (Object.FindFirstObjectByType<InvincibilityCheat>() != null)
            {
                return;
            }

            var root = new GameObject(ROOT_NAME);
            Object.DontDestroyOnLoad(root);
            root.AddComponent<InvincibilityCheat>();
        }

        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (!IsModifierPressed(keyboard))
            {
                return;
            }

            if (!keyboard.iKey.wasPressedThisFrame)
            {
                return;
            }

            if (!TryResolveHealth(out var health))
            {
                return;
            }

            health.ToggleInvincible();
            Debug.Log($"Invincibility: {(health.isInvincible ? "ON" : "OFF")}");
        }

        private bool TryResolveHealth(out HealthComponent health)
        {
            if (m_health != null && m_health.gameObject.activeInHierarchy)
            {
                health = m_health;
                return true;
            }

            if (m_player == null)
            {
                m_player = Object.FindAnyObjectByType<PlayerController>();
            }

            if (m_player == null || m_player.core == null)
            {
                health = null;
                return false;
            }

            m_health = m_player.core.GetCoreComponent<HealthComponent>();
            health = m_health;
            return health != null;
        }

        private bool IsModifierPressed(Keyboard keyboard) =>
            keyboard.leftCtrlKey.isPressed || keyboard.rightCtrlKey.isPressed;
    }
}
