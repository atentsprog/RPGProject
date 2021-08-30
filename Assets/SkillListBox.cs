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


    internal void Init(SkillInfo skillInfo)
    {
        skillName = transform.Find("SkillName").GetComponent<Text>();
        mana = transform.Find("Mana").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();

        skillName.text = skillInfo.name;
        mana.text = skillInfo.mana.ToString();
        icon.sprite = skillInfo.Sprite;
    }
}
