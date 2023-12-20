using System;
using UnityEngine;

public class FadeTween : MonoBehaviour, ITweenAnimation
{
    [SerializeField] private TweenData _tweenData;
    private GameObject _tweenTarget;
    private Action _onCompleteCallback;

    private LTDescr _tweenDescr;
    private CanvasGroup _canvasGroup;
    private float _targetAlpha;

    public FadeTween(TweenData tweenData, float _targetAlpha, GameObject tweenTarget, Action onCompleteCallback)
    {
        _tweenData = tweenData;
        this._tweenTarget = tweenTarget;
        _onCompleteCallback = onCompleteCallback;

        _canvasGroup = _tweenTarget.GetComponent<CanvasGroup>();
    }

    public void Animate()
    {
        _tweenDescr = LeanTween.alphaCanvas(_canvasGroup, _targetAlpha, _tweenData.Time)
            .setOnComplete(() => { _onCompleteCallback?.Invoke(); });
    }

    public void StopAnimation()
    {
        _tweenDescr.updateNow();
    }
}
