using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTween : MonoBehaviour, ITweenAnimation
{
    private TweenData _tweenData;
    private GameObject _tweenTarget;
    private Action _onCompleteCallback;

    private LTDescr _tweenDescr;

    public SlideTween(TweenData tweenData, GameObject tweenTarget, Action onCompleteCallback)
    {
        _tweenData = tweenData;
        this._tweenTarget = tweenTarget;
        _onCompleteCallback = onCompleteCallback;
    }

    public void Animate()
    {
        //RectTransform rectTransform = tweenTarget.GetComponent<RectTransform>();

        _tweenDescr = LeanTween.move(_tweenTarget, _tweenData.TargetPosition, _tweenData.Time)
            .setEase(_tweenData.EaseType)
            .setOnComplete(() => { _onCompleteCallback?.Invoke(); });
    }

    public void StopAnimation()
    {

        _tweenDescr.updateNow();
    }

}
