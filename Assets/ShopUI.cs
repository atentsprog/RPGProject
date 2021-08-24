using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : Singleton<ShopUI>
{
    CanvasGroup canvasGroup;
    GameObject shopMenuGo;
    GameObject subCategoryGo;
    Text guideText;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        shopMenuGo = transform.Find("ShopMenu").gameObject;
        subCategoryGo = transform.Find("SubCategory").gameObject;

        guideText = transform.Find("GuideUI/GuideText").GetComponent<Text>();
    }
    private void OnEnable()
    {
        StageManager.GameState = GameStateType.Menu;
    }
    private void OnDisable()
    {
        StageManager.GameState = GameStateType.Play;
    }
    public float speechSpeed = 20;
    internal void ShowUI()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetUpdate(true);

        shopMenuGo.SetActive(true);
        subCategoryGo.SetActive(false);

        SetGuideText("무엇을 하시겠습니까?");
    }

    private void SetGuideText(string showText)
    {
        guideText.text = "";
        guideText.DOKill();
        guideText.DOText(showText, showText.VisibleTextLength() / speechSpeed)
            .SetUpdate(true);
    }

    private void CloseUI()
    {
        canvasGroup.DOFade(0, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
