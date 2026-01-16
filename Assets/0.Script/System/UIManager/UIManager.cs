using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Dictionary<UIGroupName, GameObject> _uiGroups;
    private UnityObjectPull<Button> _buttonPull;
    private List<Button> _buttonList;
    private Transform _pullTransform;

    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private List<UIGroup> _uiGroupList;

    private void Awake()
    {
        _uiGroups = new Dictionary<UIGroupName, GameObject>();
        _pullTransform = transform.Find("UIObjectPull");
        _buttonList = new List<Button>();
        _buttonPull = new UnityObjectPull<Button>(_buttonPrefab, 10, _pullTransform);
        
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

    // UI그룹에서 GridLayoutGroup이 있는 오브젝트 찾아서 하위로 버튼 생성
    public void CreateButton(UIGroupName uiGroupName, int count)
    {
        foreach (Transform child in _uiGroups[uiGroupName].transform)
        {
            if(child.TryGetComponent<GridLayoutGroup>(out var panel))
            {
                while (count-- > 0)
                {
                    var obj = _buttonPull.GetPull(child);
                    _buttonList.Add(obj);
                }
            }
        }
    }
    
    public void RemoveButton(UIGroupName uiGroupName)
    {
        foreach (var button in _buttonList)
        {
            _buttonPull.Release(button);
        }
    }
}