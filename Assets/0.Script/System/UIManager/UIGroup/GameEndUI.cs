using System;
using UnityEngine;

public class GameEndUI : UIGroup
{
    [SerializeField] private GameObject _gameClear;
    [SerializeField] private GameObject _gameOver;

    protected override void Awake()
    {
        _gameClear.SetActive(false);
        _gameOver.SetActive(false);
        base.Awake();
    }

    private void OnEnable()
    {
        BattleManager.OnGameEnd += OnGameEndUI;
    }
    
    private void OnDisable()
    {
        BattleManager.OnGameEnd -= OnGameEndUI;
    }

    private void OnGameEndUI(UnitTeam winner)
    {
        switch (winner)
        {
            case UnitTeam.Player:
                _gameClear.SetActive(true);
                break;
            case UnitTeam.Enemy:
                _gameOver.SetActive(true);
                break;
        }
    }
}