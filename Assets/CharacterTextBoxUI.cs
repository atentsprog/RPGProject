using DG.Tweening;
using System;
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

    Coroutine lastHandle;
    public Coroutine ShowText(string _text, float visibleTime = 3,
        string _npcName = "지팡이 마법사", string npcSpriteName = "NPC1")
    {
        lastHandle = StartCoroutine(ShowTextCo(_text, visibleTime = 3,
        _npcName = "지팡이 마법사", npcSpriteName));
        return lastHandle;
    }

    public float npcShowDelayTime = 0.1f;
    IEnumerator ShowTextCo(string _text, float visibleTime,
        string _npcName, string npcSpriteName)
    { 
        canvasGroup.alpha = 0;

        contentsText.text = _text;
        npcName.text = _npcName;

        canvasGroup.DOFade(1, 0.5f);
        canvasGroup.DOFade(0, 0.5f).SetDelay(visibleTime);

        //NPC 표시
        nPC_Portrait.CrossFadeAlpha(0, 0, true);
        nPC_Portrait.sprite = Resources.Load<Sprite>(npcSpriteName);
        yield return new WaitForSeconds(npcShowDelayTime);
        nPC_Portrait.CrossFadeAlpha(1, 1, true);
    }

    internal void CloseUI(Coroutine handle)
    {
        if (lastHandle != handle)
            return;
        StopCoroutine(handle);

        canvasGroup.DOFade(0, 0.5f);
    }
}
