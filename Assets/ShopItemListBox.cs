using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemListBox : MonoBehaviour
{
    public Button button;
    [SerializeField] Text price;
    [SerializeField] Text itemName;
    [SerializeField] Image icon;
    internal void LinkComponent()
    {
        button      = GetComponent<Button>();
        price       = transform.Find("Price").GetComponent<Text>();
        itemName    = transform.Find("ItemName").GetComponent<Text>();
        icon        = transform.Find("icon").GetComponent<Image>();
    }
    internal void Init(ItemInfo item)
    {
        price.text = item.buyPrice.ToString();
        itemName.text = item.name;
        icon.sprite = item.Sprite;
    }
}
