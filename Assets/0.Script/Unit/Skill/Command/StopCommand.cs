using System.Collections;

public class StopCommand : ICommand
{
    private IAnimatable _animatable;
    private float _duration;
    private string _animationName;
    
    public float AfterDelay { get; }

    public StopCommand(IActable actable, ISkill skill, StopCommandSO so)
    {
        _animatable = actable;
        _duration = so.Duration;
        _animationName = so.AnimationName;
        AfterDelay =  so.AfterDelay;
    }

    
    public IEnumerator Execute(BattleInfo battleInfo)
    {
        _animatable.PlayAni(_animationName);
        yield return CoroutineManager.GetWaitTime(_duration);
    }
}