using System;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public event Action openChest;

    private bool m_isOpen;

    public bool isOpened { get => m_isOpen; }
    public string chestId { get => m_chestId; }

    [SerializeField] private string m_chestId;
    [SerializeField] private GameObject[] m_itemPrefab;
    [SerializeField] private Sprite m_openedSprite;

    private void Awake()
    {
        m_chestId ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    public bool CanInteract() =>
        !m_isOpen;

    public void Interact()
    {
        if(!CanInteract())
        {
            return;
        }

        SetOpened(true);
    }

    public void SetOpened(bool opened)
    {
        m_isOpen = opened;
        //TODO drop items sounds animation
        //TODO objserver
        GetComponent<SpriteRenderer>().sprite = m_openedSprite;
        openChest?.Invoke();
    }
}