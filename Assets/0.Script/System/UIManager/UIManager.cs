using System.Collections.Generic;
using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    private Dictionary<UIGroupName, UIGroup> _uiGroups;

    [SerializeField] private List<UIGroup> _uiGroupList;

    private void Awake()
    {
        _uiGroups = new Dictionary<UIGroupName, UIGroup>();
        foreach (var uiGroup in _uiGroupList)
        {
            _uiGroups.Add(uiGroup.UIGroupName, uiGroup);
        }
    }

    // UI그룹을 출력 or 해제 메서드
    public void UpdateUI(UIGroupName uiGroupName, bool active)
    {
        if(!active)
            ResetPullParnets(uiGroupName);
        
        if (_uiGroups.TryGetValue(uiGroupName, out var uiGroup))
            uiGroup.gameObject.SetActive(active);
        else
            Debug.LogError(uiGroupName + "라는 키값을 UI매니저에서 찾지 못함");
    }

    private void ResetPullParnets(UIGroupName uiGroupName) => _uiGroups[uiGroupName].ResetPullParnets();
}