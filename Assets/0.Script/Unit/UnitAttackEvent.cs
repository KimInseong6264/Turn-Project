using System;
using UnityEngine;

public class UnitAttackEvent : MonoBehaviour
{
    // 타격 타이밍에 호출될 액션
    public event Action OnAttackHitStarted;

    // Unity Animation Window에서 Event로 추가할 함수
    public void OnStartAttackHit() => OnAttackHitStarted?.Invoke();
    public void ClearOnAttackHitStarted() => OnAttackHitStarted = null;
}