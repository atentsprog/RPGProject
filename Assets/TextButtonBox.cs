using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TextButtonBox : MonoBehaviour
{
    public Text text;
    public Button button;

    public void LinkComponent()
    {
        text = GetComponentInChildren<Text>(true);
        button = GetComponent<Button>();
    }
}