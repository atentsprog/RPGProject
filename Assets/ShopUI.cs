using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//
public partial class ShopUI : Singleton<ShopUI>
{
    CanvasGroup canvasGroup;
    GameObject shopMenuGo;
    GameObject subCategoryGo;
    Text guideText;
    
    TextButtonBox categoryBaseBox;
    Button guildOkButton;

    void Awake()
    {
        InitBuyUI();

        guildOkButton = transform.Find("GuideUI/OkButton").GetComponent<Button>();
        guildOkButton.gameObject.SetActive(false);

        canvasGroup = GetComponent<CanvasGroup>();
        shopMenuGo = transform.Find("ShopMenu").gameObject;
        subCategoryGo = transform.Find("SubCategory").gameObject;
        shopMenuGo.SetActive(true);
        subCategoryGo.SetActive(false);

        guideText = transform.Find("GuideUI/GuideText").GetComponent<Text>();
        transform.Find("CloseButton/CloseButtonIcon").GetComponent<Button>()
            .onClick.AddListener(CloseUI);

        // Buy, Sell, Craft, Exit
        InitCategory();

        void InitCategory()
        {
            categoryBaseBox = transform.Find("ShopMenu/BookCover/Category/MenuCategoryBox")
                .GetComponent<TextButtonBox>();

            //"Buy", ShowBuyUI
            List<Tuple<string, UnityAction>> commandList = new List<Tuple<string, UnityAction>>();
            commandList.Add(new Tuple<string, UnityAction>("Buy", ShowBuyUI));
            commandList.Add(new Tuple<string, UnityAction>("Sell", ShowSellUI));
            commandList.Add(new Tuple<string, UnityAction>("Craft", ShowCraftUI));
            commandList.Add(new Tuple<string, UnityAction>("Exit", CloseUI));

            categoryBaseBox.LinkComponent();
            //
            categoryBaseBox.gameObject.SetActive(true);
            foreach (var item in commandList)
            {
                var newButton = Instantiate(categoryBaseBox, categoryBaseBox.transform.parent);

                newButton.text.text = item.Item1;
                newButton.button.onClick.AddListener(item.Item2);
            }
            categoryBaseBox.gameObject.SetActive(false);
        }
    }

    private void ShowCraftUI()
    {
        throw new NotImplementedException();
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
        shopMenuGo.GetComponent<CanvasGroup>().alpha = 1;
        subCategoryGo.SetActive(false);

        SetGuideText("무엇을 하시겠습니까?");
    }

    private void SetGuideText(string showText, Action action = null)
    {
        print(showText);
        guideText.text = "";
        guideText.DOKill();
        guideText.DOText(showText, showText.VisibleTextLength() / speechSpeed)
            .SetUpdate(true);


        if (action == null)
            guildOkButton.gameObject.SetActive(false);
        else
        {
            guildOkButton.gameObject.SetActive(true);
            guildOkButton.onClick.RemoveAllListeners();
            guildOkButton.onClick.AddListener(()=> { action(); });
        }
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
