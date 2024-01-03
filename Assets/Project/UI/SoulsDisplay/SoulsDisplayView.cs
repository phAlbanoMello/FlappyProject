using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulsDisplayView : View
{
    [SerializeField] private TextMeshProUGUI _soulsText;
    [SerializeField] private TextMeshProUGUI _highestScoreText;

    [Header("Text Animation")]
    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private float _jumpScale = 1.2f;
    private int _currentValue;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventBus.Subscribe<ScoreChangedEvent>(HandleScoreChanged);
        EventBus.Subscribe<PlayerDiedEvent>(HandleGameOver);
        EventBus.Subscribe<NewHighestScoreEvent>(HandleNewHighestScore);
    }

    private void HandleNewHighestScore(NewHighestScoreEvent score)
    {
        UpdateTextValue(_highestScoreText, score.Score);
    }

    private void HandleGameOver(PlayerDiedEvent @event)
    {
        EventBus.Unsubscribe<ScoreChangedEvent>(HandleScoreChanged);
        EventBus.Unsubscribe<PlayerDiedEvent>(HandleGameOver);
    }

    private void HandleScoreChanged(ScoreChangedEvent scoreChangedEvent)
    {
        _currentValue = int.Parse(_soulsText.text);
        UpdateTextValue(_soulsText, scoreChangedEvent.Score);
    }

    public void UpdateTextValue(TextMeshProUGUI textComponent, int newValue)
    {
        _currentValue = int.Parse(textComponent.text);
        LeanTween.value(gameObject, _currentValue, newValue, _animationDuration)
            .setOnUpdate((float val) =>
            {
                textComponent.text = Mathf.RoundToInt(val).ToString();
            })
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() =>
            {
                _currentValue = newValue;
                ApplyJumpEffect(textComponent.gameObject);
            });
    }
    private void ApplyJumpEffect(GameObject componentGameObject)
    {
        LeanTween.scale(componentGameObject, Vector3.one * _jumpScale, 0.1f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                LeanTween.scale(componentGameObject, Vector3.one, 0.1f)
                    .setEase(LeanTweenType.easeInQuad);
            });
    }
}
