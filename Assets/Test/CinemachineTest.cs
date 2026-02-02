using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinemachineTest : MonoBehaviour
{
    public CinemachineCamera camra;
    
    public CinemachineTargetGroup targetGroup;
    
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;

    private void Start()
    {
        camra = GetComponent<CinemachineCamera>();
    }

    private void Update()
    {
        if(Keyboard.current.aKey.isPressed)
        {
            camra.Priority.Value = 0;
        }
        if(Keyboard.current.sKey.isPressed)
        {
            targetGroup.Targets.Clear();
            AddTargetToGroup(target1);
            AddTargetToGroup(target2);
            camra.Priority.Value = 30;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            targetGroup.Targets.Clear();
            AddTargetToGroup(target3);
            AddTargetToGroup(target4);
            camra.Priority.Value = 30;
        }
           
    }
    
    public void AddTargetToGroup(Transform newTarget, float weight = 1f, float radius = 2f)
    {
        if (targetGroup == null) return;

        // 1. 새로운 Target 구조체 생성 및 데이터 할당
        CinemachineTargetGroup.Target t = new CinemachineTargetGroup.Target
        {
            Object = newTarget,
            Weight = weight,
            Radius = radius
        };

        // 2. 기존 타겟 리스트를 가져와서 추가 (v3에서는 직접 접근 및 수정 가능)
        // 주의: v3에서는 Targets 속성이 리스트 형태입니다.
        targetGroup.Targets.Add(t);
    }
}
