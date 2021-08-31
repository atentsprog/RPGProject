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
        //print(eventData);
        QuickItemUseBox fromQuickItemUseBox = eventData.pointerDrag.GetComponent<QuickItemUseBox>();
        if (fromQuickItemUseBox != null)
        {
            // 스왑 바꾸자. 
            // 기존에 있던거랑 fromQuickItemUseBox랑 정보를 바꾸자.
            var thisInventoryItemInfo = itembox.inventoryItemInfo;
            var fromInventoryItemInfo = fromQuickItemUseBox.itembox.inventoryItemInfo;
            // 기존(this)걸 바꾸자.
            SetIconAndSaveQuickSlotData(fromInventoryItemInfo
                , fromInventoryItemInfo != null ? fromInventoryItemInfo.uid : 0
                , itembox, index);
             
            // from에 있는걸 바꾸자.
            SetIconAndSaveQuickSlotData(thisInventoryItemInfo
                , thisInventoryItemInfo != null ? thisInventoryItemInfo.uid : 0
                , fromQuickItemUseBox.itembox, fromQuickItemUseBox.index);

            return;
        }

        SkillDeckBox skillDeckBox = eventData.pointerDrag.GetComponent<SkillDeckBox>();
        if (skillDeckBox != null)
        {
            InventoryItemInfo inventoryItemInfo = skillDeckBox.skillInfo.GetInventoryItemInfo();
            QuickSlotUI.Instance.ClearSlot(QuickSlotType.Skill, inventoryItemInfo.id);
            SetIconAndSaveQuickSlotData(inventoryItemInfo, inventoryItemInfo.uid, itembox, index);
            return;
        }

        ItemBox fromItemBox = eventData.pointerDrag.GetComponent<ItemBox>();
        int itemUid = fromItemBox.inventoryItemInfo.uid;
        // 기존에 같은 uid가 들어가 있었으면 해제하자.
        QuickSlotUI.Instance.ClearSlot(QuickSlotType.Item, itemUid);
        SetIconAndSaveQuickSlotData(fromItemBox.inventoryItemInfo, itemUid, itembox, index);       
    }

    private void SetIconAndSaveQuickSlotData(InventoryItemInfo setInventoryItemInfo, int saveItemUid
        , ItemBox itembox, int index)
    {
        itembox.Init(setInventoryItemInfo);
        UserData.Instance.itemData.data.quickItemUIDs[index] = saveItemUid;
    }

    public int index;
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
        //print(number.text);
        if (itembox.inventoryItemInfo == null)
            return;

        if (endTime > Time.realtimeSinceStartup)
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
        endTime = Time.realtimeSinceStartup + coolTimeSeconds;

        while (endTime > Time.realtimeSinceStartup)
        {
            float remainTime = endTime - Time.realtimeSinceStartup;
            //remainTimeText1.text = ((int)(remainTime + 1)).ToString();
            //remainTimeText2.text = (remainTime + 1).ToString("0.0");
            coolTimeText.text = remainTime.ToString("0.0");
            float remainPercent = remainTime / coolTimeSeconds;   // 1.0 -> 0.0
            coolTimeFilled.fillAmount = remainPercent;       // 1 -> 1
            yield return null;
        }
        coolTimeText.text = "";
        coolTimeFilled.fillAmount = 0;

        transform.DOPunchScale(Vector3.one * 0.15f, 0.5f)
            .SetUpdate(true);

        // 소비 아이템인경우 수량을 줄이자.
        // 소비 아이템이냐?
        bool isConsumable = itembox.inventoryItemInfo.ItemInfo.itemType == ItemType.Consume; 
        if(isConsumable)
        {
            UserData.Instance.RemoveItem(itembox.inventoryItemInfo, 1);
            if (itembox.inventoryItemInfo.count <= 0)
            {
                // UserData에서도 삭제하자. 
                itembox.inventoryItemInfo = null;
            }

            itembox.Init(itembox.inventoryItemInfo);
        }
    }
}
