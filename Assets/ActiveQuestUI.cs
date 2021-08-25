using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuestUI : Singleton<ActiveQuestUI>
{
    public TextButtonBox baseItem;
    void Awake()
    {
        baseItem = transform.Find("BG/Item").GetComponent<TextButtonBox>();
        baseItem.LinkComponent();
    }
     
    private void Start()
    {
        RefreshQuestList();
    }
    public List<GameObject> questBoxs = new List<GameObject>();
    public void RefreshQuestList()
    {
        var activeIds = UserData.Instance.questData.data.acceptIds;
        var activeQuests = ItemDB.Instance.GetQuestInfo(activeIds);
        questBoxs.ForEach(x => Destroy(x));
        questBoxs.Clear();
        baseItem.gameObject.SetActive(true);
        foreach (var item in activeQuests)
        {
            var newItem = Instantiate(baseItem, baseItem.transform.parent);
            newItem.text.text = item.questTitle;
            questBoxs.Add(newItem.gameObject);
        }
        baseItem.gameObject.SetActive(false);
    }
}
