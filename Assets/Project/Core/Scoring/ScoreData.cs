using UnityEngine;

public class ScoreData : MonoBehaviour
{
    private int currentScore;
    private int previousScore;

    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public int PreviousScore { get => previousScore; set => previousScore = value; }
}