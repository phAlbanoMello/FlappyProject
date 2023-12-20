using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextColorFading : MonoBehaviour, ITweenAnimation
{
    [SerializeField] private Color _startColor = Color.white;
    [SerializeField] private Color _endColor = Color.black;
    [SerializeField] private float _fadeDuration = 2f;
    [SerializeField] private float _delayBetweenLoops = 1f;

    private TextMeshProUGUI _textMeshObject;
    private LTDescr _animation;

    public void Initialize()
    {
        _textMeshObject = GetComponent<TextMeshProUGUI>();
    }

    public void Animate()
    { 
        StartColorFadingLoop();
    }

    public void StopAnimation()
    { 
        if (_animation != null) {
            _animation.destroyOnComplete = true;
        }
    }

    void StartColorFadingLoop()
    {
        if (_textMeshObject == null){return;}

        _textMeshObject.color = _startColor;

        _animation = LeanTween.value(gameObject, _startColor, _endColor, _fadeDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnUpdate((Color updatedColor) =>
            {
                _textMeshObject.color = updatedColor;
            })
            .setOnComplete(() =>
            {
                Color tempColor = _startColor;
                _startColor = _endColor;
                _endColor = tempColor;
                LeanTween.delayedCall(gameObject, _delayBetweenLoops, StartColorFadingLoop);
            });
    }
}

