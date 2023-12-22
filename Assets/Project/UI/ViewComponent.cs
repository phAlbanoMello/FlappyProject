using System;
using System.IO.Pipes;
using TMPro;
using UnityEngine;

public class ViewComponent : MonoBehaviour
{
    [SerializeField] private bool _animate;

    [SerializeField] private TweenData _tweenData;
    [SerializeField] private AnimationStyle _animationStyle;

    private Vector2 _originalPosition;
    private ITweenAnimation _tweenAnimation;
    
    public bool isAnimated { get { return _animate; }}

    public ITweenAnimation TweenAnimation { get => _tweenAnimation; }

    public void Initialize()
    {
        StoreOriginalPosition();

        _tweenAnimation = GetComponent<ITweenAnimation>();

        if (TweenAnimation != null)
        {
            TweenAnimation.Initialize();
            TweenAnimation.AnimateLoop();
        }
    }

    private void StoreOriginalPosition()
    {
        _originalPosition = gameObject.transform.localPosition;
    }

    public void Show()
    {
     
    }
    public void Hide()
    {

    }

    public enum AnimationStyle
    {
        Fade,
        Slide
    }
}

