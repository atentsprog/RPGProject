using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    QuickItemUseBox baseBox;
    void Awake()
    {
        baseBox = GetComponentInChildren<QuickItemUseBox>();
        baseBox.LinkComponent();
        baseBox.gameObject.SetActive(true);

        for (int i = 0; i < 10; i++)
        {
            var newButton = Instantiate(baseBox, baseBox.transform.parent);
        }
        baseBox.gameObject.SetActive(false);
    }
}
