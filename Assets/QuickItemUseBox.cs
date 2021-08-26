using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ItemBox))]
[RequireComponent(typeof(ShortcutButton))]
public class QuickItemUseBox : MonoBehaviour, IDropHandler
{
    public Text coolTimeText;
    public Image coolTimeFilled;

    public ItemBox itembox;
    public Text number;
    public void OnDrop(PointerEventData eventData)
    {
        print(eventData);
        ItemBox fromItemBox = eventData.pointerDrag.GetComponent<ItemBox>();
        itembox.Init(fromItemBox.inventoryItemInfo);

        int itemUid = fromItemBox.inventoryItemInfo.uid;
        UserData.Instance.itemData.data.quickItemUIDs[index] = itemUid;
    }

    int index;
    internal void Init(int _index, InventoryItemInfo inventoryItemInfo
        , string keybindingString)
    {
        index = _index;
        itembox.Init(inventoryItemInfo);

        number.text = keybindingString.Replace("<Keyboard>/", "");

        GetComponent<ShortcutButton>().shortcutKey
            = new InputAction("key", InputActionType.Button, keybindingString);
        GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        print(number.text);
        if (itembox.inventoryItemInfo == null)
            return;

        if (endTime > Time.time)
            return;

        StartCoroutine(StartCoolTimeCo());
    }

    internal void LinkComponent()
    {
        number = transform.Find("Number").GetComponent<Text>();
        itembox = GetComponent<ItemBox>();
        itembox.LinkComponent();
        coolTimeText = transform.Find("CoolTimeText").GetComponent<Text>();
        coolTimeFilled = transform.Find("CoolTimeFilled").GetComponent<Image>();
    }


    float endTime;
    private IEnumerator StartCoolTimeCo()
    {
        float coolTimeSeconds = 3; 
        endTime = Time.time + coolTimeSeconds;

        while (endTime > Time.time)
        {
            float remainTime = endTime - Time.time;
            //remainTimeText1.text = ((int)(remainTime + 1)).ToString();
            //remainTimeText2.text = (remainTime + 1).ToString("0.0");
            coolTimeText.text = remainTime.ToString("0.0");
            float remainPercent = remainTime / coolTimeSeconds;   // 1.0 -> 0.0
            coolTimeFilled.fillAmount = remainPercent;       // 1 -> 1
            yield return null;
        }
        coolTimeText.text = "";
        coolTimeFilled.fillAmount = 0;

        transform.DOPunchScale(transform.localScale, 0.5f);
    }
}
