using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public enum GameStateType
    {
        None,
        Play,
        Menu,
    }
    private GameStateType gameState;

    public static GameStateType GameState { get => Instance.gameState;
        set
        {
            if (Instance.gameState == value)
                return;

            Instance.gameState = value;

            float timeScale = Instance.gameState == GameStateType.Menu ? 0 : 1;

            if(timeScale != Time.timeScale)
            {
                Debug.Log($"timeScale : {Time.timeScale} -> {timeScale}");
                Time.timeScale = timeScale;

                if (Time.timeScale == 0)
                {
                    cinemachineVirtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
                    foreach (var item in cinemachineVirtualCameras)
                        item.gameObject.SetActive(false);
                }
                else
                {
                    foreach (var item in cinemachineVirtualCameras)
                        item.gameObject.SetActive(true);
                }
            }
        }
    }
    static CinemachineVirtualCamera[] cinemachineVirtualCameras;
}
