using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Skill01 : Skill
    {
        public Skill01(ISkillable unit) : base(unit)
        {
            _name = "에너미 스킬1";
            _coinCount = 1;
            _coinValue = 3;
            
            Debug.Log("================================");
            Debug.Log("<color=red>스킬이름</color>" + _name);
            Debug.Log("<color=red>스킬코인수</color>" + _coinCount);
            Debug.Log("<color=red>스킬코인벨류</color>" + _coinValue);
            Debug.Log("================================");
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