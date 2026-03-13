using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class PresentationTeleportCheat : MonoBehaviour
{
    private const string ROOT_NAME = "[PRESENTATION TELEPORT CHEAT]";

    private readonly Dictionary<int, PresentationTeleportPoint> m_points = new();

    private Player m_player;
    private Collider2D m_playerCollider;

    private TeleportMover m_teleportMover;
    private TeleportNotifier m_teleportNotifier;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Object.FindFirstObjectByType<PresentationTeleportCheat>() != null)
        {
            return;
        }

        var cheatRoot = new GameObject(ROOT_NAME);
        Object.DontDestroyOnLoad(cheatRoot);
        cheatRoot.AddComponent<PresentationTeleportCheat>();
    }

    private void Awake()
    {
        m_teleportMover = new TeleportMover();
        m_teleportNotifier = new TeleportNotifier();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        RefreshPoints();
    }

    private void OnDisable() =>
        SceneManager.sceneLoaded -= OnSceneLoaded;

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null || !IsModifierPressed(keyboard))
        {
            return;
        }

        int shortcutNumber = GetPressedShortcutNumber(keyboard);
        if (shortcutNumber < 0)
        {
            return;
        }

        TeleportToShortcut(shortcutNumber);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_player = null;
        m_playerCollider = null;

        RefreshPoints();
    }

    private void RefreshPoints()
    {
        m_points.Clear();

        var points = Object.FindObjectsByType<PresentationTeleportPoint>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None);

        foreach (var point in points)
        {
            if (point == null)
            {
                continue;
            }

            if (m_points.ContainsKey(point.shortcutNumber))
            {
                Debug.LogWarning($"Presentation teleport shortcut Ctrl + {point.shortcutNumber} is already assigned.");
                continue;
            }

            m_points.Add(point.shortcutNumber, point);
        }
    }

    private void TeleportToShortcut(int shortcutNumber)
    {
        if (!m_points.TryGetValue(shortcutNumber, out var point) || point == null)
        {
            return;
        }

        if (!TryResolvePlayer(out var player, out var playerCollider))
        {
            return;
        }

        var targetPosition = point.position;

        m_teleportMover.Move(playerCollider, targetPosition);
        m_teleportNotifier.Notify(player.gameObject, targetPosition);
    }

    private bool TryResolvePlayer(out Player player, out Collider2D playerCollider)
    {
        player = m_player;
        playerCollider = m_playerCollider;

        if (player != null &&
            playerCollider != null &&
            player.gameObject.activeInHierarchy)
        {
            return true;
        }

        m_player = Object.FindAnyObjectByType<Player>();
        if (m_player == null)
        {
            player = null;
            playerCollider = null;
            return false;
        }

        m_playerCollider = m_player.GetComponent<Collider2D>();
        if (m_playerCollider == null)
        {
            m_playerCollider = m_player.GetComponentInChildren<Collider2D>();
        }

        if (m_playerCollider == null)
        {
            player = null;
            playerCollider = null;
            return false;
        }

        player = m_player;
        playerCollider = m_playerCollider;
        return true;
    }

    private bool IsModifierPressed(Keyboard keyboard) =>
        keyboard.leftCtrlKey.isPressed || keyboard.rightCtrlKey.isPressed;

    private int GetPressedShortcutNumber(Keyboard keyboard)
    {
        if (WasShortcutPressedThisFrame(keyboard.digit1Key, keyboard.numpad1Key))
        {
            return 1;
        }

        if (WasShortcutPressedThisFrame(keyboard.digit2Key, keyboard.numpad2Key))
        {
            return 2;
        }

        if (WasShortcutPressedThisFrame(keyboard.digit3Key, keyboard.numpad3Key))
        {
            return 3;
        }

        if (WasShortcutPressedThisFrame(keyboard.digit4Key, keyboard.numpad4Key))
        {
            return 4;
        }

        if (WasShortcutPressedThisFrame(keyboard.digit5Key, keyboard.numpad5Key))
        {
            return 5;
        }

        if (WasShortcutPressedThisFrame(keyboard.digit6Key, keyboard.numpad6Key))
        {
            return 6;
        }

        if (WasShortcutPressedThisFrame(keyboard.digit7Key, keyboard.numpad7Key))
        {
            return 7;
        }

        if (WasShortcutPressedThisFrame(keyboard.digit8Key, keyboard.numpad8Key))
        {
            return 8;
        }

        if (WasShortcutPressedThisFrame(keyboard.digit9Key, keyboard.numpad9Key))
        {
            return 9;
        }

        return -1;
    }

    private bool WasShortcutPressedThisFrame(KeyControl digitKey, KeyControl numpadKey) =>
        digitKey.wasPressedThisFrame || numpadKey.wasPressedThisFrame;
}
