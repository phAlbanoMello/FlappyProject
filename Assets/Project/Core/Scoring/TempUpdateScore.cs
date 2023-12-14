using TMPro;
using UnityEngine;

public class TempUpdateScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI previousScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }
    public void SetPreviousScoreText(string text)
    {
        previousScoreText.text = text;
    }
}
