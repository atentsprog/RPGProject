using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemBox : MonoBehaviour
{
    public Image icon;
    public Button button;
    public Text count;
    public GameObject activeGo;
    public void LinkComponent()
    {
        if(icon == null)
            icon = transform.Find("Icon").GetComponent<Image>();
        if (icon == null)
            icon = GetComponentInChildren<Image>();

        if (button == null)
            button = GetComponent<Button>();

        if (count == null)
            count = GetComponentInChildren<Text>();

        if (activeGo == null)
            activeGo = transform.Find("ActiveState")?.gameObject;
    }

    public InventoryItemInfo inventoryItemInfo;
    internal void Init(InventoryItemInfo item)
    {
        inventoryItemInfo = item;
        if (item != null)
        {
            icon.enabled = true;
            if (item.type == QuickSlotType.Item)
            {
                icon.sprite = item.ItemInfo.Sprite;
                icon.SetNativeSize();
                count.text = item.count.ToString();
                icon.transform.localScale = Vector3.one;
            }
            else
            {
                icon.sprite = item.SkillInfo.Sprite;
                icon.SetNativeSize();
                icon.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                count.text = "";
            }
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
            count.text = "";
        }
    }
}
