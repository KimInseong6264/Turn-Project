using System;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    [field: SerializeField] public UIGroupName UIGroupName { get; private set; }

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }
    
    protected virtual void OnOpen() {}
    protected virtual void OnClose() => gameObject.SetActive(false);
}

public enum UIGroupName
{
    UnitSelectUI, ActSelectUI
}