using System;

public class SoulsRecovery : PickUp
{
    private DateTime _gameStartTime;
    private ScoreModifier _scoreModifier;
    internal bool _isEnabled;

    public override void Init(SpawnableObjectData data)
    { 
        base.Init(data);
        _scoreModifier = GetComponent<ScoreModifier>();
    }

    internal void SetGameStartTime(DateTime gameStartTime)
    {
        _gameStartTime = gameStartTime;
        int elapsedTimeInSeconds = CalculateSecondsPassed(_gameStartTime);
        SetSpawnDelay(elapsedTimeInSeconds);
    }
    internal void SetScore(int score)
    {
        _scoreModifier.ScoreValue = score;
        _isEnabled = true;
    }
    private int CalculateSecondsPassed(DateTime eventDateTime)
    {
        TimeSpan timePassed = DateTime.Now - eventDateTime;
        return (int)timePassed.TotalSeconds;
    }
    protected override void DisablePickUp()
    {
        base.DisablePickUp();
        CancelMovement();
        _isEnabled = false;
    }

}
