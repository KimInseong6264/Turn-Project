using System;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    [field: SerializeField] public UIGroupName UIGroupName { get; private set; }

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }
}

public enum UIGroupName
{
    UnitSelectUI, ActSelectUI, TargetSelectUI
}