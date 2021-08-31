using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDeckBox : MonoBehaviour, IDropHandler
{
    [SerializeField] Sprite enableSprite;
    [SerializeField] Sprite disableSprite;

    Image bgImage;

    DeckStateType deckState;

    Text skillName;
    Text level;
    Image icon;
    public SkillInfo skillInfo;
    public void OnDrop(PointerEventData eventData)
    {
        skillInfo = eventData.pointerDrag.GetComponent<SkillListBox>().skillInfo;
        SetUI(skillInfo);
    }
    private void SetUI(SkillInfo skillInfo)
    {
        icon.sprite = skillInfo.Sprite;
        skillName.text = skillInfo.name;

        int nLevel = 0;
        UserSKillInfo userSKillInfo = UserData.Instance.skillData
            .data.skills.Find(x => x.id == skillInfo.id);
        if (userSKillInfo != null)
            nLevel = userSKillInfo.level;
        print(nLevel);
        this.level.text = nLevel.ToString();
        SetActiveUi(true);
    }
    private void SetActiveUi(bool state)
    {
        skillName.enabled = level.enabled = icon.enabled = state;
    }
    internal void Init(DeckStateType _deckState)
    {
        bgImage = transform.Find("Bg").GetComponent<Image>();
        skillName = transform.Find("SkillName").GetComponent<Text>();
        level = transform.Find("Level").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        SetActiveUi(false);

         deckState = _deckState;
        bgImage.sprite = deckState == DeckStateType.Enable ?
            enableSprite : disableSprite;
    }
}
