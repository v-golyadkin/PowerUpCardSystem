using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _currentLevel = 1;

    private GameState _currentGameState;
    
    public event Action<GameState> OnGameStateChanged;
    public int CurrentLevel => _currentLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeGameState(GameState.CardSelection); // test
            _currentLevel++;
        } 
    }

    public enum GameState
    {
        Playing,
        CardSelection
    }

    public void ChangeGameState(GameState state)
    {
        _currentGameState = state;
        OnGameStateChanged?.Invoke(state);
        HandleStateChanged();
    }

    private void HandleStateChanged()
    {
        switch (_currentGameState)
        {
            case GameState.Playing:
                CardManager.Instance.HideCardSelectionUI();
                break;

            case GameState.CardSelection:
                CardManager.Instance.ShowCardSelectionUI();
                break;

            default:
                break;
        }
    }
}
