using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTextBoxUI : Singleton<CharacterTextBoxUI>
{
    Image nPC_Portrait;
    Text npcName;
    Text contentsText;
    CanvasGroup canvasGroup;
    void Start()
    {
        nPC_Portrait = transform.Find("NPC_Portrait").GetComponent<Image>();
        npcName = transform.Find("Name/Bg/Name").GetComponent<Text>();
        contentsText = transform.Find("ContentsText").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowText(string _text, float visibleTime = 3,
        string _npcName = "지팡이 마법사", string npcSpriteName = "NPC1")
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        contentsText.text = _text;
        npcName.text = _npcName;
        nPC_Portrait.sprite = Resources.Load<Sprite>(npcSpriteName);

        canvasGroup.DOFade(0, 0.5f).SetDelay(visibleTime);
    }
}
