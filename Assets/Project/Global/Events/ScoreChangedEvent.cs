using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChangedEvent
{
    public int Score { get; private set; }
    public ScoreChangedEvent(int score)
    {
        Score = score;
    }
}
