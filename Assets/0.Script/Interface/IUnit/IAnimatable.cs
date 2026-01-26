using UnityEngine;

public interface IAnimatable
{
    Animator GetAnimator { get; }
    void PlayAni(string animationName);
}