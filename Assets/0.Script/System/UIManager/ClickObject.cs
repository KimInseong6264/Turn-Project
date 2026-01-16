using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 리펙토링 반드시 진행해야 함
/// </summary>
public class ClickObject : MonoBehaviour ,IPointerClickHandler, IClickable
{
    public void OnPointerClick(PointerEventData eventData) => OnCklick();

    public virtual void OnCklick()
    {
        Debug.Log("클릭실행" + gameObject.name);
    }
}