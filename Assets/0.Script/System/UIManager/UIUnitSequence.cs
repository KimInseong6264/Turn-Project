using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitSequence : MonoBehaviour
{
    private List<Image> _images;
    [SerializeField] private Image _sequencePrefab;


    private void Awake()
    {
        _images = new List<Image>();
    }

    private void OnEnable()
    {
        _images.Clear();
        
        if(BattleManager.Instance != null)
            OnCreateUI();
    }

    private void OnDisable()
    {
        OnDestroyUI();
    }

    private void OnCreateUI()
    {
        foreach (var battleSequence in BattleManager.Instance.BattleSequence.Values)
        {
            Image imageObj = Instantiate(_sequencePrefab, transform);
            imageObj.sprite = battleSequence.Attacker.Data.UnitIcon;

            var imageText = imageObj.GetComponentInChildren<Text>();
            if (imageText != null)
                imageText.text = battleSequence.Speed.ToString();
            
            _images.Add(imageObj);
        }
    }

    private void OnDestroyUI()
    {
        foreach (var imageObj in _images)
            Destroy(imageObj.gameObject);
    }
}