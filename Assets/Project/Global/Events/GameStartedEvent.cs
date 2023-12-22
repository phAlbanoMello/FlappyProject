using System;

public class GameStartedEvent
{
    public DateTime gameStartTime {  get; private set; }

    public GameStartedEvent()
    {
        gameStartTime = DateTime.Now;
    }
}
