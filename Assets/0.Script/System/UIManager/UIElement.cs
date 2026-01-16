using System;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour , IClickable
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void OnCklick()
    {
        Debug.Log("클릭실행" + gameObject.name);
    }
}