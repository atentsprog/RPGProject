using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TextButtonBox : MonoBehaviour
{
    public Button button;
    public Text text;
    void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
    }
}
