using UnityEngine;

public class AfterImageSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    [SerializeField] private float m_activeTime = 0.1f;
    [SerializeField] private float m_alphaSet = 0.8f;
    [SerializeField] private float m_alphaDecay;

    [SerializeField] private Color m_color;

    private Transform m_player;
    private SpriteRenderer m_playerSpriteRenderer;

    private float m_timeActivated;
    private float m_alpha;

    private void OnEnable()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_playerSpriteRenderer = m_player.GetComponent<SpriteRenderer>();   

        m_alpha = m_alphaSet;
        m_spriteRenderer.sprite = m_playerSpriteRenderer.sprite;
        transform.position = m_player.position;
        transform.rotation = m_player.rotation;
        m_timeActivated = Time.time;
    }

    private void Update()
    {
        m_alpha -= m_alphaDecay * Time.deltaTime;
        m_color = new Color(1f, 1f, 1f, m_alpha);
        m_spriteRenderer.color = m_color;

        if(Time.time >= (m_timeActivated + m_activeTime))
        {
            Pool.Instance.AddToPool(this.gameObject);
        }
    }
}