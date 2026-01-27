using UnityEngine;

public interface IAnimatable
{
    Animator MyAnimator { get; }
    void PlayAni(string animationName);
}