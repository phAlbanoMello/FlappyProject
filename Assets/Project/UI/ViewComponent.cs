using System;
using System.IO.Pipes;
using TMPro;
using UnityEngine;

public class ViewComponent : MonoBehaviour
{
    public enum AnimationDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private bool _animate;
    [SerializeField] private AnimationDirection _hideDirection = AnimationDirection.Up;
    [SerializeField] private LeanTweenType _easeType = LeanTweenType.linear;
    [SerializeField] private float _animationDuration = 1f;

    private Vector2 _originalPosition;
    private ITweenAnimation _tweenAnimation;

    public bool isAnimated { get { return _animate; }}

    private void Awake()
    {
        StoreOriginalPosition();
        _tweenAnimation = GetComponent<ITweenAnimation>();
    }

    public void Initialize()
    {
        if (_tweenAnimation != null && isAnimated)
        {
            _tweenAnimation.Initialize();
            _tweenAnimation.Animate();
        }
    }

    private void StoreOriginalPosition()
    {
        _originalPosition = gameObject.transform.localPosition;
    }

    public void AnimateOut()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 direction = GetDirectionVector(_hideDirection);
        Vector2 targetPosition = (Vector2)rectTransform.localPosition + direction * GetScreenDimension();

        LeanTween.move(rectTransform, targetPosition, _animationDuration)
            .setEase(_easeType)
            .setOnComplete(() => {
                if(_tweenAnimation != null)
                    _tweenAnimation.StopAnimation(); 
            });
    }

    public void AnimateIn(Action onCompleteCallback)
    {
        onCompleteCallback += () => {
            if(_tweenAnimation != null)
            _tweenAnimation.Animate(); 
        };

        RectTransform rectTransform = GetComponent<RectTransform>();

        LeanTween.move(rectTransform, _originalPosition, _animationDuration)
            .setEase(_easeType)
            .setOnComplete(onCompleteCallback);
    }

    private Vector2 GetDirectionVector(AnimationDirection direction)
    {
        switch (direction)
        {
            case AnimationDirection.Up:
                return Vector2.up;
            case AnimationDirection.Down:
                return Vector2.down;
            case AnimationDirection.Left:
                return Vector2.left;
            case AnimationDirection.Right:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    float GetScreenDimension()
    {
        if (_hideDirection == AnimationDirection.Up || _hideDirection == AnimationDirection.Down)
        {
            return Screen.height;
        }
        else
        {
            return Screen.width;
        }
    }
}

