

public static class DamageCalculator
{
    public static int CalculateDamage(BattleInfo battleInfo, int finalCoinValue)
    {
        float attLevel = battleInfo.Attacker.Data.AttLevel;
        float defLevel = battleInfo.Target.Data.DefLevel;
        
        return (int)(finalCoinValue + (attLevel - defLevel) / 3);
    }
}