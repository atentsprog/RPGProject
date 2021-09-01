using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

public class EditorOptionConfig : EditorWindow
{
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

    private void SetNormalTextures()
    {
        // 선택한거 한개만 대상으로 해보자.
        foreach (var item in Selection.objects)
        {
            Material mat = (Material)item;
            if (mat == null)
                continue;
            SetNormalTexture(mat);
        }

        void SetNormalTexture(Material mat)
        {
            string mainPath = AssetDatabase.GetAssetPath(mat.mainTexture);
            string normalPath = mainPath.Replace(".png", "_n.png");
            Texture normalTexture = AssetDatabase.LoadAssetAtPath<Texture>(normalPath);
            mat.SetTexture("_DetailNormalMap", normalTexture);
        }
    }
}