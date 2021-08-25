using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    Text text;
    public float animationDuration = 1.0f;
    void Awake()
    {
        text = GetComponentInChildren<Text>();
        UserData.Instance.onChangedGold += ChangedGold;
    }
    private void OnEnable()
    {
        ChangedGold(0, UserData.Instance.accountData.data.gold);
    }
    private void ChangedGold(int oldValue, int newValue)
    {
        DOTween.To(() => oldValue, (x) =>
        {
            text.text = x.ToString();
        }, newValue, animationDuration)
            .SetUpdate(true);
    }
}
