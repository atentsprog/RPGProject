using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Text price;
    public Text itemName;
    public Image icon;

    void Awake()
    {
        price = transform.Find("Price").GetComponent<Text>();
        itemName  = transform.Find("Name").GetComponent<Text>();
        icon  = transform.Find("Icon").GetComponent<Image>();
    }
}
