using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TextButtonBox : MonoBehaviour
{
    public Text text;

    public void LinkComponent()
    {
        text = GetComponentInChildren<Text>(true);
    }

    public void Init(string str)
    {
        text.text = str;
    }
}
