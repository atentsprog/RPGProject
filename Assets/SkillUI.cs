using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    QuickUseItemBox baseBox;


    readonly string[] Shortcuts = new string[]{
        "<Keyboard>/1"
        , "<Keyboard>/2"
        , "<Keyboard>/3"
        , "<Keyboard>/4"
        , "<Keyboard>/5"
        , "<Keyboard>/6"
        , "<Keyboard>/7"
        , "<Keyboard>/8"
        , "<Keyboard>/9"
        , "<Keyboard>/0" };

    List<QuickUseItemBox> quickUseItemBoxes = new List<QuickUseItemBox>();
    private void Awake()
    {
        baseBox = GetComponentInChildren<QuickUseItemBox>();
        baseBox.gameObject.SetActive(true);
        for (int i = 0; i < Shortcuts.Length; i++)
        {
            QuickUseItemBox newBox = Instantiate(baseBox, baseBox.transform.parent);
            newBox.Init(i, Shortcuts[i]);
            quickUseItemBoxes.Add(newBox);
        }
        baseBox.gameObject.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < quickUseItemBoxes.Count; i++)
        {
            var item = quickUseItemBoxes[i];
            InventoryItemInfo inventoryItemInfo = UserData.Instance.itemData.data.GetShortcutItem(i);
            item.SetItem(inventoryItemInfo);
        }
    }
}
