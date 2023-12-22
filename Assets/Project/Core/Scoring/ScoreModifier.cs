using UnityEngine;

public class ScoreModifier : MonoBehaviour
{
    [SerializeField] private int _scoreValue;

    public int ScoreValue { get => _scoreValue; set => _scoreValue = value; }
}
