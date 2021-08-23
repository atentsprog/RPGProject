using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTextBoxUI : Singleton<CharacterTextBoxUI>
{
    Image portrait;
    Text nameText;
    Text contentsText;
    CanvasGroup canvasGroup;
    void Start()
    {
        portrait = transform.Find("Portrait").GetComponent<Image>();
        nameText = transform.Find("Name/NameText").GetComponent<Text>();
        contentsText = transform.Find("ContentsText").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public float talkSpeed = 20;
    public void ShowText(string _text, float visibleTime = 3, string _name = "NPC"
        , string portraitSpriteName = "NPC1")
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.DOKill();
        canvasGroup.DOFade(1, 0.5f);

        contentsText.text = "";
        contentsText.DOText(_text, _text.VisibleTextLength() / talkSpeed).SetUpdate(true);
        nameText.text = _name;
        portrait.sprite = Resources.Load<Sprite>("NPC/" + portraitSpriteName);
        //portrait.sprite =(Sprite)Resources.Load("NPC/" + portraitSpriteName, typeof(Sprite));

        canvasGroup.DOFade(0, 0.5f).SetDelay(visibleTime);
    }

    internal void CloseUI()
    {
        canvasGroup.DOFade(0, 0.5f).SetUpdate(true)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
