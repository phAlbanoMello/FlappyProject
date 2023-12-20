using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulsDisplayView : View
{
    private TextMeshProUGUI _soulsText;

    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private float _jumpScale = 1.2f;
    private int _currentValue;

    public override void Initialize()
    {
        base.Initialize();
        _soulsText = GetComponentInChildren<TextMeshProUGUI>();

        EventBus.Subscribe<ScoreChangedEvent>(HandleScoreChanged);
    }

    private void HandleScoreChanged(ScoreChangedEvent scoreChangedEvent)
    {
        _currentValue = int.Parse(_soulsText.text);
        UpdateTextValue(scoreChangedEvent.Score);
    }

    public void UpdateTextValue(int newValue)
    {
        _currentValue = int.Parse(_soulsText.text);
        LeanTween.value(gameObject, _currentValue, newValue, _animationDuration)
            .setOnUpdate((float val) =>
            { 
                _soulsText.text = Mathf.RoundToInt(val).ToString();
            })
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() =>
            {
                _currentValue = newValue;
                ApplyJumpEffect();
            });
    }
    private void ApplyJumpEffect()
    {
        LeanTween.scale(_soulsText.gameObject, Vector3.one * _jumpScale, 0.1f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                LeanTween.scale(_soulsText.gameObject, Vector3.one, 0.1f)
                    .setEase(LeanTweenType.easeInQuad);
            });
    }
}
