using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipUI : MonoBehaviour
{
    EquipItemBox baseBox;

    void Awake()
    {
        baseBox = GetComponentInChildren<EquipItemBox>(true);
        baseBox.gameObject.SetActive(true);
        baseBox.LinkComponent();
        for (int i = 0; i < 8; i++)
        {
            Transform parent;
            //parent = transform.Find(i < 4 ? "EqupItem/Right" : "EqupItem/Left");
            if (i < 4)
                parent = transform.Find("EqupItem/Right");
            else
                parent = transform.Find("EqupItem/Left");

            EquipItemBox newBox = Instantiate(baseBox, parent);

            int itemUID = UserData.Instance.itemData.data.equipItemUIDs[i];
            InventoryItemInfo inventoryItemInfo = UserData.Instance.GetItem(itemUID);

            newBox.Init(i, inventoryItemInfo);
            newBox.itemBox.button.onClick.AddListener(() => OnClick(newBox));
        }
        baseBox.gameObject.SetActive(false);
    }

    private void OnClick(EquipItemBox newBox)
    {
        //print(newBox.index);
        ItemInfoUI.Instance.ShowUI(newBox.itemBox.inventoryItemInfo);
    }
}
