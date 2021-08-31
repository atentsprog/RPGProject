using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDeckBox : MonoBehaviour, IDropHandler
{
    [SerializeField] Sprite normalSprite;
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
        UserData.Instance.skillData.data.deckIDs[index] = skillInfo.id;
        SetUI(skillInfo);
    }
    public void SetUI(SkillInfo skillInfo)
    {
        if (skillInfo != null)
        {
            icon.sprite = skillInfo.Sprite;
            skillName.text = skillInfo.name;
            UserSKillInfo userSKillInfo = UserData.Instance.skillData
                .data.skills.Find(x => x.id == skillInfo.id);
            if (userSKillInfo != null)
                level.text = $"Lv{userSKillInfo.level}";
            else
                level.text = "미획득";
            bgImage.sprite = normalSprite;
            SetActiveUi(true);
        }
        else
        {
            icon.sprite = null;
            skillName.text = string.Empty;
            SetActiveUi(false);
            bgImage.sprite = deckState == DeckStateType.Enable ?
                enableSprite : disableSprite;
        }
    }
    private void SetActiveUi(bool state)
    {
        skillName.enabled = level.enabled = icon.enabled = state;
    }
    int index;
    internal void Init(int _index, DeckStateType _deckState)
    {
        index = _index;
        bgImage = transform.Find("Bg").GetComponent<Image>();
        skillName = transform.Find("SkillName").GetComponent<Text>();
        level = transform.Find("Level").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        SetActiveUi(false);

         deckState = _deckState;
        bgImage.sprite = deckState == DeckStateType.Enable ?
            enableSprite : disableSprite;
    }

    internal void SetCardInfo(SkillInfo skillInfo)
    {
        throw new NotImplementedException();
    }
}
