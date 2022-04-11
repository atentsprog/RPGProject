using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUI : Singleton<QuickSlotUI>
{
    QuickItemUseBox baseBox;

    public string[] keyBinding = new string[]{
        "<Keyboard>/1",
        "<Keyboard>/2",
        "<Keyboard>/3",
        "<Keyboard>/4",
        "<Keyboard>/5",
        "<Keyboard>/6",
        "<Keyboard>/7",
        "<Keyboard>/8",
        "<Keyboard>/9",
        "<Keyboard>/0"
        };

    List<QuickItemUseBox> quickSlots = new List<QuickItemUseBox>(10);
    void Awake()
    {
        baseBox = GetComponentInChildren<QuickItemUseBox>();
        baseBox.LinkComponent();
        baseBox.gameObject.SetActive(true);
        for (int i = 0; i < keyBinding.Length; i++)
        {
            var newButton = Instantiate(baseBox, baseBox.transform.parent);
            var quickSlotInfo = UserData.Instance.itemData.data.quickItemUIDs[i];
            if (quickSlotInfo == null)
                continue;

            InventoryItemInfo inventoryItemInfo = null;
            if (quickSlotInfo.uidOrId > 0)
            {
                if (quickSlotInfo.type == QuickSlotType.Item)
                {
                    int itemUID = quickSlotInfo.uidOrId;
                    inventoryItemInfo = UserData.Instance.GetItem(itemUID);
                }
                else
                {
                    int skillID = quickSlotInfo.uidOrId;
                    inventoryItemInfo = ItemDB.GetSkillInfo(skillID).GetInventoryItemInfo();
                }
            }

            newButton.Init(i, inventoryItemInfo, keyBinding[i]);
            quickSlots.Add(newButton);
        }
        baseBox.gameObject.SetActive(false);
    }
    internal void ClearSlot(QuickSlotType type, int itemUid)
    {
        //quickSlots.Find(x => x.itembox.inventoryItemInfo != null && x.itembox.inventoryItemInfo.uid == itemUid)
        //    ?.itembox.Init(null);
        var list = quickSlots.FindAll(x =>
            x.itembox.inventoryItemInfo != null
            && x.itembox.inventoryItemInfo.type == type
            && x.itembox.inventoryItemInfo.uid == itemUid);

        foreach (var item in list)
        {
            item.itembox.Init(null);
            UserData.Instance.itemData.data.quickItemUIDs[item.index].uidOrId = 0;
        }


        // 위에 코드랑 동일한 결과.
        //quickSlots.FindAll(x =>
        //    x.itembox.inventoryItemInfo != null
        //    && x.itembox.inventoryItemInfo.uid == itemUid)
        //    .ForEach(x => x.itembox.Init(null));
    }

    internal void UpdateItemInfo(InventoryItemInfo existItem)
    {
        quickSlots.Find(x => x.itembox.inventoryItemInfo != null
            && x.itembox.inventoryItemInfo.uid == existItem.uid)
            ?.itembox.Init(existItem);
    }
}
