using System;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    [field: SerializeField] public UIGroupName UIGroupName { get; private set; }
    
    [SerializeField] protected Transform _objectPullTransform;

    
    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    
    public virtual void ResetPullParnets() {}
}


public enum UIGroupName
{
    GameStart, UnitSelectUI, ActSelectUI, TargetSelectUI, GameEnd
}