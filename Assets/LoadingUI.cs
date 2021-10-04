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
    public int minimumFrame = 10;
    private IEnumerator ShowUICo(AsyncOperation result)
    {
        Image progressBar = transform.Find("ProgressBar").GetComponent<Image>();
        Text percent = transform.Find("Percent").GetComponent<Text>();


        // 최소한 10프레임 로딩바 진행 보여주자. -> 
        // 0 ~ 100%까지 올리자. 
        // 로딩이 먼저 되어도 기다리자.
        // 로딩이 나중에 되면 
        for (int i = 0; i < minimumFrame || result.isDone == false; i++)
        {
            float percentFloat = (float)i / minimumFrame; 
            // 1 * / 10 = 0.1f
            // 1 * / 100 = 0.01f;

            if (percentFloat > result.progress)
                percentFloat = result.progress;

            percent.text = $"{(percentFloat * 100):0}" + "%"; //  소수점을 표시 하고 싶다면 :0 -> :0.0, :0.00, :0.표시하고싶은 소수점 자리수 만큼 0채우면 된다
            //print(percent.text);
            progressBar.fillAmount = percentFloat;
            yield return null;
        }

        percent.text = "100%";
        progressBar.fillAmount = 1;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 0.5f).OnComplete( ()=> gameObject.SetActive(false));
    }
}
