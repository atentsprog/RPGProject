using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 시작하면 자식 오브젝트 모두 활성하 한다
/// </summary>
public class ChildActivator : MonoBehaviour
{
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
