using System;
using UnityEngine;

public class UnitAttackEvent : MonoBehaviour
{
    // 타격 타이밍에 호출될 액션
    public event Action OnAttackHitStarted;
    // 공격이 완전히 끝났을 때 호출할 함수 (커맨드 종료 알림용)
    // public event Action OnAttackHitFinished;

    // Unity Animation Window에서 Event로 추가할 함수
    public void OnStartAttackHit() => OnAttackHitStarted?.Invoke();
    public void ClearOnAttackHiStarted() => OnAttackHitStarted = null;
    
    // public void OnEndAttackHit() => OnAttackHitFinished?.Invoke();
}
