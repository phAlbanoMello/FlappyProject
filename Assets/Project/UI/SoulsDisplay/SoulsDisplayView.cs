using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulsDisplayView : View
{
    private TextMeshProUGUI _soulsText;

    [Header("Text Animation")]
    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private float _jumpScale = 1.2f;
    private int _currentValue;

    public override void Initialize()
    {
        base.Initialize();

        GetTextMeshObject();

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventBus.Subscribe<ScoreChangedEvent>(HandleScoreChanged);
        EventBus.Subscribe<PlayerDiedEvent>(HandleGameOver);
    }

    private void HandleGameOver(PlayerDiedEvent @event)
    {
        EventBus.Unsubscribe<ScoreChangedEvent>(HandleScoreChanged);
        EventBus.Unsubscribe<PlayerDiedEvent>(HandleGameOver);
    }

    private void GetTextMeshObject()
    {
        _soulsText = GetComponentInChildren<TextMeshProUGUI>();
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
