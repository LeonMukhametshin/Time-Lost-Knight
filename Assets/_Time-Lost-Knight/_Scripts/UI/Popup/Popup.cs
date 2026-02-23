using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public bool open { get; private set; }

    [SerializeField] private CanvasGroup m_bodyAlphaGroup;
    [SerializeField] private RectTransform m_body;
    [SerializeField] private Button m_button;

    private Vector2 m_targetBodyPosition;
    private Vector2 m_startShift;

    private Sequence m_animation;

    private void OnDestroy() => 
        KillCurrentAnimationIfActive();

    private void Awake()
    {
        m_targetBodyPosition = m_body.anchoredPosition;
        m_startShift = new Vector2(m_targetBodyPosition.x, -Screen.height / 2);
    }

    public void Show()
    {
        KillCurrentAnimationIfActive();
        m_animation = DOTween.Sequence();

        m_animation
            .Append(m_bodyAlphaGroup.DOFade(1, 0.5f))
            .Join(m_body.DOAnchorPos(m_targetBodyPosition, 1f).From(m_startShift))
            .Append(m_button.transform.DOScale(1, 0.5f).From(0).SetEase(Ease.OutBounce));

        open = true;
    }

    public void Hide(Action collback)
    {
        KillCurrentAnimationIfActive();
        m_animation = DOTween.Sequence();

        m_animation
           .Append(m_bodyAlphaGroup.DOFade(0, 1f).From(1))
           .Join(m_body.DOAnchorPos(m_startShift, 1f).From(m_targetBodyPosition))
           .OnComplete(() => collback?.Invoke());

        open = false;
    }

    public bool InAnimation =>
        m_animation != null && m_animation.active;

    private void KillCurrentAnimationIfActive()
    {
        if(InAnimation)
        {
            m_animation.Kill();
        }
    }
}

