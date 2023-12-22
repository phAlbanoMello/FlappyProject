
using System;

public interface ITweenAnimation
{
    //NOTE: maybe these functions would work better as actions that can be checked if 
    //they have assigned functions
    public virtual void Setup() { }
    public virtual void Initialize() { }
    public virtual void AnimateIn(Action onCompleteCallback = null) { }
    public virtual void AnimateOut(Action onCompleteCallback = null) { }
    public virtual void AnimateLoop() { }
    public virtual void StopAnimation() { }
    public virtual void Pause() { }
    public virtual void Resume() { }
}
