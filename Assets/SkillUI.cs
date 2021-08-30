using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public override void ShowUI()
    {
        base.ShowUI();

        LinkComponent();

        print("skillUI showUI");
    }

    List<SkillDeckBox> skillDeckBoxes = new List<SkillDeckBox>(8);
    bool isCompleteLink = false;
    private void LinkComponent()
    {
        if (isCompleteLink)
            return;

        // 초기화 하자.
        //레벨 1 : 5개 사용가능, 
        // 2 : 6, 3 : 7, 4 : 8
        deckBase = GetComponentInChildren<SkillDeckBox>(true);
        int level = UserData.Instance.accountData.data.level;
        for (int i = 0; i < 8; i++)
        {
            //1 : 4 + level = 5;
            DeckStateType deckState = 4 + level > i ? DeckStateType.Enable : DeckStateType.Disable;
            var newItem = Instantiate(deckBase, deckBase.transform.parent);
            newItem.Init(deckState);
            skillDeckBoxes.Add(newItem);
        }
        deckBase.gameObject.SetActive(false);

        isCompleteLink = true;
    }
}

public enum DeckStateType
{
    Disable, 
    Enable,
    Used,
}