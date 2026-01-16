using System;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    [field: SerializeField] public UIGroupName UIGroupName { get; private set; }
    [field: SerializeField] public GameObject UIGameObject { get; private set; }
}

public enum UIGroupName
{
    ActSellectUI
}