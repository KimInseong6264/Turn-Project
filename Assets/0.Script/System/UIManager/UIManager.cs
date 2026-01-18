using System.Collections.Generic;
using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    private Dictionary<UIGroupName, GameObject> _uiGroups;

    [SerializeField] private List<UIGroup> _uiGroupList;

    private void Awake()
    {
        _uiGroups = new Dictionary<UIGroupName, GameObject>();
        foreach (var uiGroup in _uiGroupList)
        {
            _uiGroups.Add(uiGroup.UIGroupName, uiGroup.gameObject);
        }
    }

    // UI그룹을 출력 or 해제 메서드
    public void UpdateUI(UIGroupName uiGroupName, bool active)
    {
        if (_uiGroups.TryGetValue(uiGroupName, out var uiGroup))
            uiGroup.SetActive(active);
        else
            Debug.LogError(uiGroupName + "라는 키값을 UI매니저에서 찾지 못함");
    }
}