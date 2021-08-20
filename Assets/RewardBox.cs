using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardBox : MonoBehaviour
{
    Image icon;
    Text count;

    public void Init()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        count = transform.Find("Count").GetComponent<Text>();
    }
}
