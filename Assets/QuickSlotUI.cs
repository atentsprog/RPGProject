using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var item = UserData.Instance.itemData.data.quickItemUIDs[i];

            InventoryItemInfo inventoryItemInfo;
            if (item.type == QuickSlotType.Skill)
            {
                var userSkillData = UserData.Instance.skillData.data.skills.Where(x => x.id == item.id).FirstOrDefault();
                inventoryItemInfo = userSkillData.GetInventoryItemInfo();
            }
            else
            {
                int itemUID = item.id;
                inventoryItemInfo = UserData.Instance.GetItem(itemUID);
            }
            newButton.Init(i, inventoryItemInfo, keyBinding[i]);
            quickSlots.Add(newButton);
        }
        baseBox.gameObject.SetActive(false);
    }
    internal void ClearSlot(int itemUidOrID)
    {
        var list = quickSlots.FindAll(x =>
            x.itembox.inventoryItemInfo != null
            && x.itembox.inventoryItemInfo.uid == itemUidOrID);

        foreach (var item in list)
        {
            item.itembox.Init(null);
            UserData.Instance.itemData.data.quickItemUIDs[item.index].id = 0;
        }
    }

    internal void UpdateItemInfo(InventoryItemInfo existItem)
    {
        quickSlots.Find(x => x.itembox.inventoryItemInfo != null
            && x.itembox.inventoryItemInfo.uid == existItem.uid)
            ?.itembox.Init(existItem);
    }
}
