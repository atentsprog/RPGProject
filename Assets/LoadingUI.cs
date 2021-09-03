﻿using DG.Tweening;
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
    internal void ShowUI()
    {
        gameObject.SetActive(true);
    }
    internal void SetProgress(AsyncOperation result)
    {
        StartCoroutine(ShowUICo(result));
    }
    private IEnumerator ShowUICo(AsyncOperation result)
    {
        Image progressBar = transform.Find("ProgressBar").GetComponent<Image>();
        Text percent = transform.Find("Percent").GetComponent<Text>();
        float fakeProgress = 0;
        //var originalPriority = Application.backgroundLoadingPriority;
        //Application.backgroundLoadingPriority = ThreadPriority.Low;
        while (result.isDone == false)
        {
            fakeProgress += 0.1f;
            float realProgress = result.progress;
            float showProgress = fakeProgress < 0.9f ? fakeProgress : realProgress;

            percent.text = Mathf.RoundToInt(showProgress * 100) + "%";
            print($"{showProgress}, {realProgress}, {fakeProgress}");
            progressBar.fillAmount = showProgress;
            yield return null;
        }
        //Application.backgroundLoadingPriority = originalPriority;

        percent.text = "100%";
        progressBar.fillAmount = 1;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 0.5f).OnComplete( ()=> gameObject.SetActive(false));
    }
}
