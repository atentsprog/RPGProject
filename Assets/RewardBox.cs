using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardBox : MonoBehaviour
{
    public Image icon;
    public Text count;

    public void Init()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        count = transform.Find("Count").GetComponent<Text>();
    }

    internal void Init(RewardInfo item)
    {
        icon.sprite = ItemDB.GetItemInfo(item.itemID).Sprite;
        count.text = item.count.ToString();
    }
}
