using System.Collections;

namespace Enemy
{
    public class Skill02 : SkillBase
    {
        public Skill02(ISkillable unit) : base(unit)
        {
        }

        public override IEnumerator Execute()
        {
            yield break;
        }
    }
}