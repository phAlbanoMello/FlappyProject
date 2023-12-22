
public class PlayerDiedEvent
{
    public int Souls { get; private set; }
   
    public PlayerDiedEvent(int previousSoulsBeforeDeath){
        Souls = previousSoulsBeforeDeath;
    }
}
