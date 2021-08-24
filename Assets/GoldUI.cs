using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GoldUI : MonoBehaviour
{
    Text text;
    public float animationDuration = 0.5f;
    private void ChangedGold(int oldValue, int newValue)
    {
        DOTween.To(() => oldValue, (x) =>
        {
            text.text = x.ToString();
            //print(x.ToString());
        }, newValue, animationDuration)
            .SetUpdate(true);
    }

    void Awake()
    {
        text = GetComponentInChildren<Text>();
        UserData.Instance.changedGold += ChangedGold;
        ChangedGold(0, UserData.Instance.playerData.data.gold);
    }
}
