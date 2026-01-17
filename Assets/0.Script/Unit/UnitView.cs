using System;
using UnityEngine;

public class UnitView : MonoBehaviour, IClickable
{
    public UnitPresenter Presenter { get; private set; }
    
    //
    public UnitDataSO unitData;
    //
    
    private void Awake()
    {
        Presenter = new UnitPresenter(unitData, this);
    }


    private void Update()
    {
        Presenter.Tick();
    }

    public void SetSkill(SkillType skillType) => Presenter.SetSkill(skillType);

    public void SetSkill01()
    {
        Presenter.SetSkill(SkillType.Skill01);
        Debug.Log($"현재 스킬 타입: {Presenter.Skill.GetType().Name}");
    }

    public void SetSkill02()
    {
        Presenter.SetSkill(SkillType.Skill02);
        Debug.Log("스킬변경" + Presenter.Skill);
    }

    public void OnStartCklick()
    {
        Debug.Log("클릭대상" + unitData.Name);
    }
}