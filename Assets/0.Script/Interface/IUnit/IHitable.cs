using UnityEngine;

public interface IHitable
{
    void OnHit(BattleInfo battleInfo, int damage, KnockbackInfo? knockbackInfo = null);
}