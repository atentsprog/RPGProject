using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillListBox : MonoBehaviour
{
    Text skillName;
    Text mana;
    Image icon;

    public SkillInfo skillInfo;
    internal void Init(SkillInfo _skillInfo)
    {
        skillInfo = _skillInfo;
        skillName = transform.Find("SkillName").GetComponent<Text>();
        mana = transform.Find("Mana").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();

        if (skillInfo != null)
        {
            skillName.text = skillInfo.name;
            mana.text = skillInfo.mana.ToString();
            icon.sprite = skillInfo.Sprite;
        }
        else
        {
            skillName.text = string.Empty;
            mana.text = string.Empty;
            icon.sprite = null;
        }
    }
}
