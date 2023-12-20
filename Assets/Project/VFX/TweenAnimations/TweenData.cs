using UnityEngine;

[System.Serializable]
public class TweenData
{
    [SerializeField] private Vector2 _targetPosition;
    [SerializeField] private LeanTweenType _easeType = LeanTweenType.linear;
    [SerializeField] private float _time = 1f;

    public TweenData(Vector2 targetPosition, LeanTweenType easeType, float time)
    {
        _targetPosition = targetPosition;
        _easeType = easeType;
        _time = time;
    }

    public Vector2 TargetPosition { get => _targetPosition; }
    public LeanTweenType EaseType { get => _easeType;  }
    public float Time { get => _time; }
}
