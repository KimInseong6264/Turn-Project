using System.Collections;
using UnityEngine;

namespace Player
{
    public class Skill02 : SkillBase
    {
        public Skill02(SkillDataSO unitData, ISkillable unit) : base(unitData, unit)
        {
        }

        public override IEnumerator Execute()
        {
            Debug.Log("================================");
            Debug.Log("<color=red>스킬이름</color>" + _name);
            Debug.Log("<color=red>스킬코인수</color>" + _coinCount);
            Debug.Log("<color=red>스킬코인벨류</color>" + _coinValue);
            Debug.Log("================================");
            yield break;
        }
    }
}