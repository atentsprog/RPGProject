using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListBox : MonoBehaviour
{
    public Text text;

    internal void Init()
    {
        text = transform.Find("Text").GetComponent<Text>();
    }

    public QuestInfo questInfo;
    internal void Init(QuestInfo item)
    {
        questInfo = item;
        text.text = item.questTitle;
    }
}
