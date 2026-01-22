using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private bool m_isOpen;

    public bool isOpened { get => m_isOpen; }
    public string chestId { get => m_chestId; }

    [SerializeField] private string m_chestId;
    [SerializeField] private GameObject m_itemPrefab;
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

        Open();
    }

    private void Open()
    {
        SetOpened(true);

        if(m_itemPrefab)
        {
            GameObject droppedItem = Instantiate(m_itemPrefab, transform.position + Vector3.down, Quaternion.identity);
        }
    }

    public void SetOpened(bool opened)
    {
        if (m_isOpen = opened)
        {
            GetComponent<SpriteRenderer>().sprite = m_openedSprite;
        }
    }
}
