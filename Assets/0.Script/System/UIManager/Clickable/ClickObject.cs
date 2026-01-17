using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 리펙토링 반드시 진행해야 함
/// </summary>
public class ClickObject : MonoBehaviour ,IPointerClickHandler, IClickable
{
    public event Action OnClick;


    protected virtual void OnDisable()
    {
        OnClick = null;
    }


    public void OnPointerClick(PointerEventData eventData) => OnStartCklick();

    public virtual void OnStartCklick()
    {
        Debug.Log("클릭실행" + gameObject.name);
        OnClick?.Invoke();
    }
}