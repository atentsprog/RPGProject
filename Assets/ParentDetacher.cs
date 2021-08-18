using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 시작하면 부모를 해제한다.
/// </summary>
public class ParentDetacher : MonoBehaviour
{
    void Awake()
    {
        //transform.parent = null; // 작동안함
        transform.SetParent(null);
    }
}
