using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoBox : MonoBehaviour
{
    public Image icon;
    public Text count;

    internal void Init()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        count = transform.Find("Count").GetComponent<Text>();
    }

    internal void Init(RewardInfo rewardItem)
    {
        icon.sprite = ItemDB.GetItemInfo(rewardItem.id).Sprite;
        icon.SetNativeSize();
        count.text = rewardItem.count.ToString();
    }
}
