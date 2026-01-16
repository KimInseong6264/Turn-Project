using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    public partial class UIManager
    {
        private UnityObjectPull<ClickObject> _buttonPull;
        private List<ClickObject> _buttonList;
        private Transform _pullTransform;
        
        [SerializeField] private ClickObject _buttonPrefab;

        // 버튼 1개를 생성하는 메서드
        public T OnCreateButton<T>(UIGroupName uiGroupName, string createTag = "") where T : ClickObject
        {
            if (createTag == "")
            {
                var obj = _buttonPull.GetPull(_uiGroups[uiGroupName].transform).AddComponent<T>();
                _buttonList.Add(obj);
                return obj;
            }

            foreach (Transform child in _uiGroups[uiGroupName].transform)
            {
                if (child.CompareTag(createTag))
                {
                    var obj = _buttonPull.GetPull(child).AddComponent<T>();
                    _buttonList.Add(obj);
                    return obj;
                }
            }
            
            Debug.LogWarning("UI 생성 위치를 찾지 못했습니다.");
            return null;
        }
        
        // UI그룹에서 GridLayoutGroup이 있는 오브젝트 찾아서 하위로 버튼 생성
        public List<ClickObject> OnCreateButton<T>(int creatCount, UIGroupName uiGroupName, string createTag = "") where T : ClickObject
        {
            InitButtonList(isClear: true);

            SetButtonList<T>(creatCount, uiGroupName, createTag);
            
            return _buttonList;
        }
        
        // 버튼리스트에 값을 갱신하는 메서드
        private void SetButtonList<T> (int creatCount, UIGroupName uiGroupName, string createTag = "") where T : ClickObject
        {
            while (creatCount-- > 0)
            {
                var obj = OnCreateButton<T>(uiGroupName, createTag);
                _buttonList.Add(obj);
            }
        }

        // 초기화
        public void InitButtonList(bool isClear)
        {
            foreach (var button in _buttonList)
            {
                if(button.gameObject.activeSelf)
                    _buttonPull.Release(button);
            }
            
            if(isClear)
                _buttonList.Clear();
        }
    }