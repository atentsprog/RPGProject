using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
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
}
