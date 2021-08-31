using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI<T> : Singleton<T> where T : MonoBehaviour
{
    CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        StageManager.GameState = GameStateType.Menu;
    }
    private void OnDisable()
    {
        StageManager.GameState = GameStateType.Play;
    }
     
    public virtual void ShowUI()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetUpdate(true);


    }

    public void CloseUI()
    {
        canvasGroup.DOFade(0, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}

public class SkillUI : BaseUI<SkillUI>
{
    SkillDeckBox deckBase;
    SkillListBox listBase;
    public override void ShowUI()
    {
        base.ShowUI();
        LinkComponent();
        print("skillUI showUI");
    }

    List<SkillDeckBox> skillDeckBoxes = new List<SkillDeckBox>(8);
    List<SkillListBox> skillListBoxes = new List<SkillListBox>();
    Text description;
    Button button;

    bool isCompleteLink = false;
    private void LinkComponent()
    {
        if (isCompleteLink)
            return;

        InitDeck();
        InitList();

        SetDeckCard();

        description = transform.Find("Down/Bg/Description").GetComponent<Text>();
        button = transform.Find("Down/Bg/Button").GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        isCompleteLink = true;
    }

    private void SetDeckCard()
    {
        for (int i = 0; i < skillDeckBoxes.Count; i++)
        {
            var item = skillDeckBoxes[i];
            int skillID = UserData.Instance.skillData.data.deck[i];
            
            SkillInfo skillInfo = ItemDB.GetSkillInfo(skillID);
            item.SetSkillInfo(skillInfo);
        }
    }

    private void OnClick()
    {
        print("가이드 텍스트 클릭함" + onClickCB);
        onClickCB.Invoke();
    }

    private void InitList()
    {
        listBase = GetComponentInChildren<SkillListBox>(true);

        var skills = ItemDB.Instance.skills;

        for (int i = 0; i < skills.Count; i++)
        { 
            var newItem = Instantiate(listBase, listBase.transform.parent);
            newItem.Init(skills[i]);
            skillListBoxes.Add(newItem);
        }
        listBase.gameObject.SetActive(false);
    }

    private void InitDeck()
    {
        // 초기화 하자.        //레벨 1 : 5개 사용가능,         // 2 : 6, 3 : 7, 4 : 8
        deckBase = GetComponentInChildren<SkillDeckBox>(true);
        int level = UserData.Instance.accountData.data.level;
        for (int i = 0; i < 8; i++)
        {
            //1 : 4 + level = 5;
            DeckStateType deckState = 4 + level > i ? DeckStateType.Enable : DeckStateType.Disable;
            var newItem = Instantiate(deckBase, deckBase.transform.parent);
            newItem.Init(i, deckState);
            skillDeckBoxes.Add(newItem);
        }
        deckBase.gameObject.SetActive(false);
    }

    Action onClickCB;
    internal void SetGuideText(string str, Action _onClickCB = null)
    {
        onClickCB = _onClickCB;

        description.DOKill();
        description.text = "";
        description.DOText(str, str.VisibleTextLength() / 20).SetUpdate(true);
        button.gameObject.SetActive(_onClickCB != null);
    }
}

public enum DeckStateType
{
    Disable, 
    Enable,
    Used,
}