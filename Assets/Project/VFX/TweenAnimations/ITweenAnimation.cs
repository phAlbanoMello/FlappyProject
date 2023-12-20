
using System;

public interface ITweenAnimation
{
    public virtual void Initialize() { }
    public virtual void Animate(Action onAnimationComplete = null) { }
    public virtual void StopAnimation() { }
    public virtual void Pause() { }
    public virtual void Resume() { }
}
