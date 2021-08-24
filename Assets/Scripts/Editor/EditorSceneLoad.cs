using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EditorSceneLoad
{
    [MenuItem("Window/W. Map1 Scene Load")]
    private static void TitleSceneLoad()
    {
        LoadScene("Map1");
    }


    //[MenuItem("Window/2. Map1 Scene Load")]
    //private static void Stage1SceneLoad()
    //{
    //    LoadScene("Map1");
    //}
    private static void LoadScene(string loadSceneName)
    {
        EditorSceneManager.OpenScene($"Assets/Scenes/{loadSceneName}.unity");
    }
}
