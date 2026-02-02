using System;
using UnityEngine;
using UnityEngine.Playables;

public class SkillActManager : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector =GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        CameraManager.OnSetTargetEnd += TimelinePlay;
        UnitSkill.OnSkillEnd += TimelineStop;
    }

    private void OnDisable()
    {
        CameraManager.OnSetTargetEnd -= TimelinePlay;
        UnitSkill.OnSkillEnd -= TimelineStop;
    }

    private void TimelinePlay()
    {
        _playableDirector.Play();
    }

    private void TimelineStop()
    {
        _playableDirector.Stop();
    }
}
