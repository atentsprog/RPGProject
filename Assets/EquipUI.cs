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
        for (int i = 0; i < 8; i++)
        {
            Transform parent;
            //parent = transform.Find(i < 4 ? "EqupItem/Right" : "EqupItem/Left");
            if (i < 4)
                parent = transform.Find("EqupItem/Right");
            else
                parent = transform.Find("EqupItem/Left");

            EquipItemBox newBox = Instantiate(baseBox, parent);
            //newBox.Init(item);
            //newBox.button.onClick.AddListener(() => OnClick(item));
        }
        baseBox.gameObject.SetActive(false);
    }
}
