using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuestUI : Singleton<ActiveQuestUI>
{
    public TextButtonBox baseItem;
    void Awake()
    {
        baseItem = transform.Find("BG/Item").GetComponent<TextButtonBox>();
    }
     
    private void Start()
    {
        RefreshQuestList();
    }

    public void RefreshQuestList()
    {
        var activeIds = UserData.Instance.questData.data.acceptIds;
        var activeQuests = ItemDB.Instance.GetQuestInfo(activeIds);

        //baseItem.LinkComponent();
        baseItem.gameObject.SetActive(true);
        foreach (var item in activeQuests)
        {
            var newItem = Instantiate(baseItem, baseItem.transform.parent);
            newItem.text.text = item.questTitle;
        }
        baseItem.gameObject.SetActive(false);
    }
}
