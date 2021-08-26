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
    public GameObject activeGo;
    public void LinkComponent()
    {
        if(button == null)
            button = GetComponent<Button>();

        if(text == null)
            text = GetComponentInChildren<Text>();

        if (activeGo == null)
            activeGo = transform.Find("ActiveState")?.gameObject;
    }
}
