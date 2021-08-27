using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillUI : Singleton<SkillUI>
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

    List<QuickItemUseBox> itemBoxes = new List<QuickItemUseBox>(10);
    void Awake()
    {
        baseBox = GetComponentInChildren<QuickItemUseBox>();
        baseBox.LinkComponent();
        baseBox.gameObject.SetActive(true);

        for (int i = 0; i < keyBinding.Length; i++)
        {
            var newButton = Instantiate(baseBox, baseBox.transform.parent);
            int itemUID = UserData.Instance.itemData.data.quickItemUIDs[i];
            InventoryItemInfo inventoryItemInfo = UserData.Instance.GetItem(itemUID);
            newButton.Init(i, inventoryItemInfo, keyBinding[i]);
        }
        baseBox.gameObject.SetActive(false);
    }

    internal void ClearBox(int itemUid)
    {
        itemBoxes.Find(x => x.itembox.inventoryItemInfo != null && x.itembox.inventoryItemInfo.uid == itemUid)
            ?.itembox.Init(null);

        //Linq로 표현 위와 같은 결과.
        //itemBoxes.Where(x => x.itembox.inventoryItemInfo != null && x.itembox.inventoryItemInfo.uid == itemUid)
        //    .FirstOrDefault()?.itembox.Init(null);
    }
}
