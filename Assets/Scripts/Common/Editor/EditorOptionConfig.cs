using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

public class EditorOptionConfig : EditorWindow
{
    [MenuItem("Tools/SetNormalTexture")]
    static void SetNormalTexture_Command()
    {
        Debug.Log("노멀 텍스쳐 적용하자");
        SetNormalTextures();
    }

    private static void SetNormalTextures()
    {
        Debug.Log("SetNormalTexture 실행");

        foreach(var item in Selection.objects)
        {
            Material mat = (Material)item;
            if (mat == null)
                continue;
            SetNormalTexture(mat);
        }
    }

    private static void SetNormalTexture(Material mat)
    {
        string mainTexPath = AssetDatabase.GetAssetPath(mat.mainTexture);
        string normalTexPath = mainTexPath.Replace(".png", "_n.png");
        Texture normalTexture = AssetDatabase.LoadAssetAtPath<Texture>(normalTexPath);
        mat.SetTexture("_BumpMap", normalTexture);
        mat.SetFloat("_BumpScale", 0.3f);
    }

    [MenuItem("Tools/Option")]
    static void Init()
    {
        GetWindow(typeof(EditorOptionConfig));
    }

    Vector2 mPos = Vector2.zero;
    void OnGUI()
    {
        mPos = GUILayout.BeginScrollView(mPos);
        if(GUILayout.Button("테스트 버튼"))
        {
            SetNormalTextures();
        }

        for (OptionType i = OptionType.StartIndex + 1; i < OptionType.LastIndex; i++)
        {
            GUILayout.BeginHorizontal();
            {
                bool tempBool = EditorOption.Options[i];

                EditorOption.Options[i] = GUILayout.Toggle(EditorOption.Options[i], i.ToString());

                if (tempBool != EditorOption.Options[i])
                {
                    string key = "DevOption_" + i;
                    EditorPrefs.SetInt(key, EditorOption.Options[i] == true ? 1 : 0);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }
}