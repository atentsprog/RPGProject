using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPortal : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;


        //맵 로딩 UI표시.
        LoadingUI.Instance.ShowUI();

        // 맵 로드 비동기로 하자
        var result = SceneManager.LoadSceneAsync(sceneName);

        // 프로그레스 설정.
        LoadingUI.Instance.SetProgress(result);
    }
}
