using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TalkAlertUI : Singleton<TalkAlertUI>
{
    Text text;
    CanvasGroup canvasGroup;
    void Start()
    {
        text = transform.Find("BG/BG/Text").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowText(string _text, float visibleTime = 3)
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        text.text = _text;

        canvasGroup.DOFade(0, 0.5f).SetDelay(visibleTime);
    }
}
