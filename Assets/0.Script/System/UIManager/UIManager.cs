using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<UIGroupName, GameObject> _uiGroups;
    
    [SerializeField] private List<UIGroup> _uiGroupList;

    private void Awake()
    {
        _uiGroups = new Dictionary<UIGroupName, GameObject>();
        foreach (var uiGroup in _uiGroupList)
        {
            uiGroup.UIGameObject.SetActive(false);
            _uiGroups.Add(uiGroup.UIGroupName, uiGroup.UIGameObject);
        }
    }

    public void UpdateUI(UIGroupName uiGroupName, bool active)
    {
        if (_uiGroups.TryGetValue(uiGroupName, out var uiGroup))
            uiGroup.SetActive(active);
        else
            Debug.LogError(uiGroupName + "라는 키값을 UI매니저에서 찾지 못함");
    }

    public void CreateUI(UIGroupName uiGroupName)
    {
    }
}