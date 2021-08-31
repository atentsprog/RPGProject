using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDeckBox : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    ItemBox itemBox;
    int index;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite enableSprite;
    [SerializeField] Sprite disableSprite;

    Image bgImage;
    DeckStateType deckState;

    Text skillName;
    Text level;
    Image icon;
    public void OnDrop(PointerEventData eventData)
    {
        if (deckState == DeckStateType.Disable)
            return;

        SkillListBox skillListBox = eventData.pointerDrag.GetComponent<SkillListBox>();
        SetSkillInfo(skillListBox.skillInfo);
        //skillListBox.skillInfo
    }

    SkillInfo skillInfo;
    public UserSKillInfo userSKillInfo;
    public void SetSkillInfo(SkillInfo _skillInfo)
    {
        skillInfo = _skillInfo;
        int skillID = 0;
        if (skillInfo != null)
        {
            skillID = skillInfo.id;
            userSKillInfo = UserData.Instance.skillData.data.skills
                .Where(x => x.id == skillID)
                .FirstOrDefault();

            if (userSKillInfo != null)
                level.text = $"Lv{userSKillInfo.level}";
            else
                level.text = "미획득";

            icon.sprite = skillInfo.Sprite;
            skillName.text = skillInfo.name;

            SetActiveDataUI(true);
            bgImage.sprite = normalSprite;

            deckState = DeckStateType.Used;

            itemBox.inventoryItemInfo = userSKillInfo.GetInventoryItemInfo();
        }
        else
        {
            SetActiveDataUI(false);
            bgImage.sprite = deckState == DeckStateType.Enable ?
                enableSprite : disableSprite;
        }

        // skillID 덱에 저장.
        UserData.Instance.skillData.data.deck[index] = skillID;
    }

    private void SetActiveDataUI(bool state)
    {
        level.enabled       = state;
        icon.enabled        = state;
        skillName.enabled   = state;
    }

    internal void Init(int _index, DeckStateType _deckState)
    {
        index = _index;
        bgImage = transform.Find("Bg").GetComponent<Image>();
        skillName = transform.Find("SkillName").GetComponent<Text>();
        level = transform.Find("Level").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        itemBox = GetComponent<ItemBox>();
        SetActiveDataUI(false);

        deckState = _deckState;
        bgImage.sprite = deckState == DeckStateType.Enable ?
            enableSprite : disableSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("click");
        if (skillInfo == null)
            return;

        bool needLearn = userSKillInfo == null;

        string skillName = skillInfo.name;
        if (needLearn)
            SkillUI.Instance.SetGuideText($"{skillName} 배우시겠습니까?", OnClickLearnSkill);
        else
        {
            //만렙이 아니면 레벨업하자.
            bool isMaxLevel = skillInfo.maxLevel <= userSKillInfo.level;
            if(isMaxLevel == false)
                SkillUI.Instance.SetGuideText($"{skillName} Lv{userSKillInfo.level+1}로 증가하시겠습니까?", OnClickLevelUp);
            else
                SkillUI.Instance.SetGuideText($"{skillName} 최대 레벨({userSKillInfo.level}) 입니다");
        }
    }

    private void OnClickLevelUp()
    {
        //SP포인트 소모 시키자.
        //유저 레벨 올리자.
        SubSkillPointAndIncreaseSkillLevel();
    }

    private void SubSkillPointAndIncreaseSkillLevel()
    {
        UserData.Instance.skillData.data.skillPoint--;
        userSKillInfo.level++;

        SkillUI.Instance.SetGuideText($"{skillInfo.name}  Lv{userSKillInfo.level}로 증가했습니다");
    }

    private void OnClickLearnSkill()
    {
        // 스킬 유저 정보에 추가하자.
        // SP포인트 소모 시키자.
        // 레벨 1로 설정하자.
        userSKillInfo = new UserSKillInfo() { id = skillInfo.id, level = 0 };
        UserData.Instance.skillData.data.skills.Add(userSKillInfo);

        SubSkillPointAndIncreaseSkillLevel();

        SkillUI.Instance.SetGuideText($"{skillInfo.name}을 배웠습니다");
    }
}
