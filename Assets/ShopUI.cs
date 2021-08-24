using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//
public class ShopUI : Singleton<ShopUI>
{
    CanvasGroup canvasGroup;
    GameObject shopMenuGo;
    GameObject subCategoryGo;
    Text guideText;
    
    TextButtonBox categoryBaseBox;
    void Awake()
    {
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

    private void ShowSellUI()
    {
        throw new NotImplementedException();
    }

    //List<GameObject> buyBaseBoxs = new List<GameObject>();
    private void ShowBuyUI()
    {
        shopMenuGo.SetActive(false);
        subCategoryGo.SetActive(true);

        // Buy, Sell, Craft, Exit
        InitCategory();

        void InitCategory()
        {
            categoryBaseBox = transform.Find("SubCategory/Left/Content/CotegoryBox")
                .GetComponent<TextButtonBox>();

            //"Buy", ShowBuyUI
            List<Tuple<string, UnityAction>> commandList = new List<Tuple<string, UnityAction>>();
            commandList.Add(new Tuple<string, UnityAction>("무기"         , () => ShowBuyList(ItemType.Weapon)));
            commandList.Add(new Tuple<string, UnityAction>("방어구"       , () => ShowBuyList(ItemType.Armor)));
            commandList.Add(new Tuple<string, UnityAction>("악세서리"     , () => ShowBuyList(ItemType.Accesory)));
            commandList.Add(new Tuple<string, UnityAction>("소비 아이템"  , () => ShowBuyList(ItemType.Consume)));
            commandList.Add(new Tuple<string, UnityAction>("재료"         , () => ShowBuyList(ItemType.Material)));

            //buyBaseBoxs.ForEach(x => Destroy(x));
            //buyBaseBoxs.Clear();

            categoryBaseBox.LinkComponent();
            categoryBaseBox.gameObject.SetActive(true);
            foreach (var item in commandList)
            {
                var newButton = Instantiate(categoryBaseBox, categoryBaseBox.transform.parent);

                newButton.text.text = item.Item1;
                newButton.button.onClick.AddListener(item.Item2);
                //buyBaseBoxs.Add(newButton.gameObject);
            }
            categoryBaseBox.gameObject.SetActive(false);
        }
    }

    private void ShowBuyList(ItemType itemType)
    {
        //print(itemType);
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
