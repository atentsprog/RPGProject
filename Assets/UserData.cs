using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserQuestData
{
    public List<int> acceptIds = new List<int>();
    public List<int> rejectIds = new List<int>();
}

public class UserData : Singleton<UserData>
{
    public PlayerPrefsData<UserQuestData> questData;
    private void Awake()
    {
        questData = new PlayerPrefsData<UserQuestData>("UserQuestData1");
    }

    private void OnDestroy()
    {
        questData.SaveData();
    }
}
