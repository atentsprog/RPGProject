using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public partial class ShopUI : Singleton<ShopUI>
{
    public enum ShopType
    {
        Buy,
        Sell,
        Craft,
    }
    ShopType currentType;

    CanvasGroup canvasGroup;
    CanvasGroup subCategoryCanvasGroup;
    CanvasGroup shopMenuCanvasGroup;
    Text guideText;


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        subCategoryCanvasGroup = transform.Find("SubCategory").GetComponent<CanvasGroup>();
        shopMenuCanvasGroup = transform.Find("ShopMenu").GetComponent<CanvasGroup>();
        subCategoryCanvasGroup.gameObject.SetActive(false);
        shopMenuCanvasGroup.gameObject.SetActive(true);

        guideText = transform.Find("GuideBg/GuideText").GetComponent<Text>();

        InitCategory();

        void InitCategory()
        {
            List<Tuple<string, UnityAction>> categoryAction = new List<Tuple<string, UnityAction>>();
            categoryAction.Add(new Tuple<string, UnityAction>("Buy", ShowBuyUI));
            categoryAction.Add(new Tuple<string, UnityAction>("Sell", ShowSellUI));
            categoryAction.Add(new Tuple<string, UnityAction>("Craft", ShowCraftUI));
            categoryAction.Add(new Tuple<string, UnityAction>("Exit", CloseUI));

            TextButtonBox menuCategoryBoxBase;
            menuCategoryBoxBase = transform.Find("ShopMenu/BookCover/Category/MenuCategoryBox")
                .GetComponent<TextButtonBox>();
            menuCategoryBoxBase.LinkComponent();

            menuCategoryBoxBase.gameObject.SetActive(true);
            foreach (var item in categoryAction)
            {
                var newButton = Instantiate(menuCategoryBoxBase, menuCategoryBoxBase.transform.parent);

                newButton.text.text = item.Item1;
                newButton.button.onClick.AddListener(item.Item2);
            }
            menuCategoryBoxBase.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StageManager.GameState = GameStateType.Menu;
    }
    private void OnDisable()
    {
        StageManager.GameState = GameStateType.Play;
    }

    internal void ShowUI()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetUpdate(true);

        SetGuideString("무엇을 하시겠습니까?");
    }

    private void CloseUI()
    {
        canvasGroup.DOFade(0, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                StageManager.GameState = GameStateType.Play;
                gameObject.SetActive(false);
            });
    }

    void ShowBuyUI() { ShowSubCategory(ShopType.Buy); }

    void ShowSellUI() { ShowSubCategory(ShopType.Sell); }
    void ShowCraftUI() { ShowSubCategory(ShopType.Craft); }

    private void ShowSubCategory(ShopType type)
    {
        currentType = type;
        print("show:" + type);
        shopMenuCanvasGroup.DOFade(0, 0.5f)
            .OnComplete( ()=>shopMenuCanvasGroup.gameObject.SetActive(false))
            .SetUpdate(true);
        subCategoryCanvasGroup.gameObject.SetActive(true);
        subCategoryCanvasGroup.DOFade(1, 0.5f)
            .SetUpdate(true);
        switch(type)
        {
            case ShopType.Buy:
                ShowBuyList();
                break;
        }
    }

    public float speechSpeed = 20;
    private void ShowBuyList()
    {
        print("구입 가능한 목록 표시하자");
        SetGuideString("구입 가능한 목록입니다");

        //// 카테고리 초기화.
        Show(currentType);
    }

    private void SetGuideString(string str)
    {
        guideText.DOKill();
        guideText.text = "";
        guideText.DOText(str, str.VisibleTextLength() / speechSpeed).SetUpdate(true);
    }
}
