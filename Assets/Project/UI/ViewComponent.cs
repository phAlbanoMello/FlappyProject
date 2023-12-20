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

    public void Initialize()
    {
        StoreOriginalPosition();
        //TODO: Create TweenAnimation here?
        _tweenAnimation = GetComponent<ITweenAnimation>();

        if (_tweenAnimation != null)
        {
            _tweenAnimation.Initialize();
            _tweenAnimation.Animate();
        }
    }

    private void StoreOriginalPosition()
    {
        _originalPosition = gameObject.transform.localPosition;
    }

    public void Show()
    {
        switch (_animationStyle)
        {
            case AnimationStyle.Fade:

                break;
            case AnimationStyle.Slide:
                break;
        }
    }
    public void Hide()
    {

    }

    public void AnimateOut()
    {
        //RectTransform rectTransform = GetComponent<RectTransform>();
        //Vector2 direction = GetDirectionVector(_hideDirection);
        //Vector2 targetPosition = (Vector2)rectTransform.localPosition + direction * GetScreenDimension();

        //LeanTween.move(rectTransform, targetPosition, _animationDuration)
        //    .setEase(_easeType)
        //    .setOnComplete(() => {
        //        if(_tweenAnimation != null)
        //            _tweenAnimation.StopAnimation(); 
        //    });
    }

    public void AnimateIn(Action onCompleteCallback)
    {
        //onCompleteCallback += () => {
        //    if(_tweenAnimation != null)
        //    _tweenAnimation.Animate(); 
        //};

        //RectTransform rectTransform = GetComponent<RectTransform>();

        //LeanTween.move(rectTransform, _originalPosition, _animationDuration)
        //    .setEase(_easeType)
        //    .setOnComplete(onCompleteCallback);
    }

    //private Vector2 GetDirectionVector(AnimationDirection direction)
    //{
    //    switch (direction)
    //    {
    //        case AnimationDirection.Up:
    //            return Vector2.up;
    //        case AnimationDirection.Down:
    //            return Vector2.down;
    //        case AnimationDirection.Left:
    //            return Vector2.left;
    //        case AnimationDirection.Right:
    //            return Vector2.right;
    //        default:
    //            return Vector2.zero;
    //    }
    //}

    //float GetScreenDimension()
    //{
    //    if (_hideDirection == AnimationDirection.Up || _hideDirection == AnimationDirection.Down)
    //    {
    //        return Screen.height;
    //    }
    //    else
    //    {
    //        return Screen.width;
    //    }
    //}


    public enum AnimationStyle
    {
        Fade,
        Slide
    }
}

