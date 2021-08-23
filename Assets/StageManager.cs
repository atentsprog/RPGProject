using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateType
{
    None,
    Play,
    Menu,
}


public class StageManager : Singleton<StageManager>
{
    [SerializeField]
    GameStateType gameState = GameStateType.None;

    public static GameStateType GameState
    {
        get => Instance.gameState;
        set
        {
            if (Instance.gameState == value)
                return;

            var oldState = Instance.gameState;
            Instance.gameState = value;

            if(value == GameStateType.Menu)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;

            Debug.Log($"gameState:{oldState} =>{value} " +
                $"timeScale:{Time.timeScale}");
        } 
    }
    private void Awake()
    {
        gameState = GameStateType.Play;
    }
}
