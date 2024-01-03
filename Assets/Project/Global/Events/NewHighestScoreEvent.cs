
public class NewHighestScoreEvent
{
    public NewHighestScoreEvent(int score)
    {
        Score = score;
    }

    public int Score { get; private set; } 
}
