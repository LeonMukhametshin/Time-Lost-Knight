using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public bool open { get; private set; }

    [SerializeField] private CanvasGroup m_bodyAlphaGroup;
    [SerializeField] private RectTransform m_body;

    [SerializeField] private float m_showTime = 0.2f;
    [SerializeField] private float m_closeTime = 0.2f;

    [SerializeField] private Button[] m_buttons;
    [SerializeField][Min(0f)] private float m_buttonAppearDuration = 0.35f;
    [SerializeField][Min(0f)] private float m_buttonStagger = 0.12f;

    private Vector2 m_targetBodyPosition;
    private Vector2 m_startShift;

    private Sequence m_animation;

    private void OnDestroy() => 
        KillCurrentAnimationIfActive();

    private void Awake()
    {
        m_targetBodyPosition = m_body.anchoredPosition;
        m_startShift = new Vector2(m_targetBodyPosition.x, -Screen.height / 2f);

        PrepareButtonsForShow();
    }

    public void Show()
    {
        KillCurrentAnimationIfActive();
        m_animation = DOTween.Sequence();

        m_animation
            .Append(m_bodyAlphaGroup.DOFade(1f, 0.5f))
            .Join(m_body.DOAnchorPos(m_targetBodyPosition, m_showTime).From(m_startShift));

        foreach (var button in m_buttons)
        {
            button.transform.localScale = Vector3.zero;
            m_animation.AppendInterval(m_buttonStagger)
                .Append(button.transform.DOScale(1f, m_buttonAppearDuration).SetEase(Ease.OutBack));
        }

        open = true;
    }

    public void Hide(Action callback)
    {
        KillCurrentAnimationIfActive();
        m_animation = DOTween.Sequence();

        m_animation
           .Append(m_bodyAlphaGroup.DOFade(0f, 1f).From(1f))
           .Join(m_body.DOAnchorPos(m_startShift, m_closeTime).From(m_targetBodyPosition))
           .OnComplete(() => callback?.Invoke());

        open = false;
    }

    public bool InAnimation =>
        m_animation != null && m_animation.active;

    private void PrepareButtonsForShow()
    {
        foreach (var button in m_buttons)
        {
            button.transform.localScale = Vector3.zero;
        }
    }

    private void KillCurrentAnimationIfActive()
    {
        if (InAnimation)
        {
            m_animation.Kill();
        }
    }
}