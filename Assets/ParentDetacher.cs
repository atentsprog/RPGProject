using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ϸ� �θ� �����Ѵ�.
/// </summary>
public class ParentDetacher : MonoBehaviour
{
    void Awake()
    {
        //transform.parent = null; // �۵�����
        transform.SetParent(null);
    }
}
