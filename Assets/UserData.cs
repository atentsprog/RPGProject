using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserQuestData
{
    public List<int> acceptIds = new List<int>();
    public List<int> rejectIds = new List<int>();
}

[System.Serializable]
public class UserItemData
{
    public List<InventoryItemInfo> item = new List<InventoryItemInfo>();
}

public class UserData : Singleton<UserData>
{
    public PlayerPrefsData<UserQuestData> questData;
    public PlayerPrefsData<UserItemData> itemData;

    //내가 구입한 아이템.

    private void Awake()
    {
        questData = new PlayerPrefsData<UserQuestData>("UserQuestData");
        itemData = new PlayerPrefsData<UserItemData>("UserItemData");
    }

    private void OnDestroy()
    {
        questData.SaveData();
    }
}
