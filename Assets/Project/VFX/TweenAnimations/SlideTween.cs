using System;
using UnityEngine;

public enum SlideDirection
{
    Up,
    Down,
    Left,
    Right
}

public class SlideTween : MonoBehaviour, ITweenAnimation
{
    [SerializeField] private LeanTweenType _easeType = LeanTweenType.easeOutQuad;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private SlideDirection _direction = SlideDirection.Right;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void AnimateIn(Action onCompleteCallback = null)
    {
        Vector3 startPos = GetStartPosition();
        Vector3 endPos = _rectTransform.anchoredPosition;

        _rectTransform.anchoredPosition = startPos;

        LeanTween.move(_rectTransform, endPos, _duration)
            .setEase(_easeType)
            .setOnComplete(() => { onCompleteCallback?.Invoke(); });
    }

    public void AnimateOut(Action onCompleteCallback = null)
    {
        Vector3 startPos = _rectTransform.anchoredPosition;
        Vector3 endPos = GetEndPosition();

        LeanTween.move(_rectTransform, endPos, _duration)
            .setEase(_easeType)
            .setOnComplete(() => { onCompleteCallback?.Invoke(); });
    }

    private Vector3 GetStartPosition()
    {
        switch (_direction)
        {
            case SlideDirection.Up:
                return new Vector3(_rectTransform.anchoredPosition.x, -_rectTransform.rect.height, 0f);
            case SlideDirection.Down:
                return new Vector3(_rectTransform.anchoredPosition.x, _rectTransform.rect.height, 0f);
            case SlideDirection.Left:
                return new Vector3(_rectTransform.rect.width, _rectTransform.anchoredPosition.y, 0f);
            case SlideDirection.Right:
                return new Vector3(-_rectTransform.rect.width, _rectTransform.anchoredPosition.y, 0f);
            default:
                return Vector3.zero;
        }
    }

    private Vector3 GetEndPosition()
    {
        switch (_direction)
        {
            case SlideDirection.Up:
                return new Vector3(_rectTransform.anchoredPosition.x, -_rectTransform.rect.height, 0f);
            case SlideDirection.Down:
                return new Vector3(_rectTransform.anchoredPosition.x, _rectTransform.rect.height, 0f);
            case SlideDirection.Left:
                return new Vector3(_rectTransform.rect.width, _rectTransform.anchoredPosition.y, 0f);
            case SlideDirection.Right:
                return new Vector3(-_rectTransform.rect.width, _rectTransform.anchoredPosition.y, 0f);
            default:
                return Vector3.zero;
        }
    }
}
