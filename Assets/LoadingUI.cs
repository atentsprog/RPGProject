using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : Singleton<LoadingUI>
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.root);
    }
    internal void ShowUI(AsyncOperation result)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowUICo(result));
    }
    private IEnumerator ShowUICo(AsyncOperation result)
    {
        Image progressBar = transform.Find("ProgressBar").GetComponent<Image>();
        Text percent = transform.Find("Percent").GetComponent<Text>();
        while(result.isDone == false)
        {
            percent.text = Mathf.RoundToInt(result.progress * 100) + "%";
            print(percent.text);
            progressBar.fillAmount = result.progress;
            yield return null;
        }

        percent.text = "100%";
        progressBar.fillAmount = 1;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 0.5f).OnComplete( ()=> gameObject.SetActive(false));
    }
}
