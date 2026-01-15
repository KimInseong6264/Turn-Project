using System.Collections;

namespace Player
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