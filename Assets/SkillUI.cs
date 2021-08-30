using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase<T> : Singleton<T> where T : MonoBehaviour
{
    CanvasGroup canvasGroup;
    protected void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected void ShowUI()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetUpdate(true);
    }

    protected void OnEnable()
    {
        StageManager.GameState = GameStateType.Menu;
    }
    protected void OnDisable()
    {
        StageManager.GameState = GameStateType.Play;
    }
    public void CloseUI()
    {
        canvasGroup.DOFade(0, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }

}
public class SkillUI : UIBase<SkillUI>
{
    new void Awake()
    {
        base.Awake();
        print("test");
    }

    new public void ShowUI()
    {
        base.ShowUI();
        print("ShowUI");
    }
}
